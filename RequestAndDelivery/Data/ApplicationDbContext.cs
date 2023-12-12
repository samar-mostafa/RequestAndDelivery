using Microsoft.EntityFrameworkCore;
using RequestAndDelivery.Data.Domain_Models;
using System.Data;

namespace RequestAndDelivery.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BranchDepartment>().HasKey(d => new { d.BranchId, d.DepartmentId });
        }

        //entities
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Branch> Branchs { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }
        public DbSet<BranchDepartment> BranchDepartments { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Delivary> Delivaries { get; set; }
        public DbSet<Request> Requests { get; set; }





    }
}
