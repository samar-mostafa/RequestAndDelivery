using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RequestAndDelivery.Data;
using RequestAndDelivery.Data.Domain_Models;
using RequestAndDelivery.Mapping;
using RequestAndDelivery.Seeds;
using System.Reflection;
using UoN.ExpressiveAnnotations.NetCore.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>
    (o => o.UseSqlServer(builder.Configuration.GetConnectionString("default")));


builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddIdentity<ApplicationUser,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
var scope= scopeFactory.CreateScope();
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
var roleManager=scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
await DefaultRoles.AddAsync(roleManager);
await DefaultUsers.AddAdminUserAsync(userManager);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
