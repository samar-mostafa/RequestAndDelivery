using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RequestAndDelivery.Data;
using RequestAndDelivery.Data.Domain_Models;
using RequestAndDelivery.Data.ViewModels;
using System.Text.Json;
using System.Linq.Dynamic.Core;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RequestAndDelivery.Controllers
{
    public class RequestsController : Controller
    {
        private readonly ApplicationDbContext db;

        public RequestsController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index(DataTableForm mdl)
        {
            var requests = db.Requests.Include(r => r.DeviceType).Where(r => r.IsDeliverd == mdl.Val || mdl.Val == null)
                   .Select(r => new RequestViewModel
                   {
                       Id = r.Id,
                       DeviceType = r.DeviceType.Type,
                       ExportNumber = r.ExportNumber,
                       IsDeliverd = r.IsDeliverd,
                       RequestDate = r.RequestDate.ToShortDateString(),
                       EmpNumber = r.EmployeeId,
                      
                   });
            ViewBag.All = requests.Count();
            ViewBag.Delivered = requests.Count(r => r.IsDeliverd);
            ViewBag.NotDelivered = requests.Count(r =>! r.IsDeliverd);
            if (mdl.Val != null)
            {
                var pageSize = int.Parse(Request.Query["length"].FirstOrDefault());
                var skip = int.Parse(Request.Query["start"]);
                var searchValue = Request.Query["search[value]"];
                //var sortingValue = Request.Query[string.Concat("columns[", Request.Query["order[0][column]"], "][name]")];
                //var sortDirection = Request.Query["order[0][dir]"];
                var searchValueNumber = 0;
                var r = int.TryParse(searchValue, out searchValueNumber);
                //requests.OrderBy(string.Concat(sortingValue, " ", sortDirection));

                var reqs = requests.AsEnumerable().Where(q => string.IsNullOrEmpty(searchValue) ? true :
                    (q.DeviceType.ToLower().Contains(searchValue.ToString().ToLower()) ||
                    q.ExportNumber.Contains(searchValue) ||
                    q.RequestDate.Contains(searchValue)));
                var data = reqs.Skip(skip).Take(pageSize).ToList();

                var recordsTotal = requests.Count();
                var jsonData = new { recordsFiltered = recordsTotal, recordsTotal, data};
                return Ok(jsonData);
            }

            else
                return View(requests.ToList());
        }

        public IActionResult Create()
        {
            ViewBag.DeviceTypes = db.DeviceTypes.Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Type });
            ViewBag.Branches =db.Branchs.Select(b=>new SelectListItem { Text = b.Name, Value = b.Id.ToString() });
            return View("Form");
        }

        [HttpPost]
        public IActionResult Create(RequestFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.DeviceTypes = db.DeviceTypes.Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Type });
                ViewBag.Branches = db.Branchs.Select(b => new SelectListItem { Text = b.Name, Value = b.Id.ToString() });
                return View("Form", model);
            }

          
            if(!db.Employees.Any(e => e.MobileNumber == model.EmployeeId))
            {
                var employee=new Employee
                {
                    MobileNumber = model.EmployeeId,
                    Name = model.EmployeeName,
                    BranchId = model.BranchId,
                    DepartmentId = model.DepartmentId
                };

                db.Employees.Add(employee);
            }

            var request = new Request
            {
                EmployeeId = model.EmployeeId,
                ExportNumber = model.ExportNumber,
                RequestDate = model.RequestDate,
                DeviceTypeId = model.DeviceTypeId
            };

            db.Requests.Add(request);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult GetEmployeeData(string id)
        {
            var emp = db.Employees
                .Where(e => e.MobileNumber == id)
                .Select(e => new EmployeeDataViewModel
                {
                    MobilePhone = e.MobileNumber,
                    Name = e.Name,                   
                    Branche = db.Branchs.Where(b => b.Id == e.BranchId).Select(b => b.Name).SingleOrDefault(),
                    Department = db.Departments.Where(d => d.Id == e.DepartmentId).Select(d => d.Name).SingleOrDefault()
                }).SingleOrDefault();
            return PartialView("_EmployeeData", emp);

        }

        public IActionResult GetEmployeeDataForJson(string id)
        {
            var emp = db.Employees
                .Where(e => e.MobileNumber == id)
                .Select(e => new EmployeeDataViewModel
                {                   
                    Name = e.Name,
                    BranchData = db.Branchs.Where(b => b.Id == e.BranchId).Select(b => new SelectListItem
                    {
                        Text = b.Name,
                        Value=b.Id.ToString()
                    }).SingleOrDefault(),
                    DepartmentData = db.Departments.Where(d => d.Id == e.DepartmentId).Select(b => new SelectListItem
                    {
                        Text = b.Name,
                        Value = b.Id.ToString()
                    }).SingleOrDefault()
                }).SingleOrDefault();
            return Json(emp);

        }
        public IActionResult GetBranchDepartments(int id)
        {
            var departments = db.BranchDepartments.Include(bd => bd.Department)
                .Where(bd => bd.BranchId == id).Select(bd => new
                {
                    Id = bd.DepartmentId,
                    Name = bd.Department.Name

                });
            return Json(departments);
        }

        //public IActionResult AllowItem(RequestFormViewModel model)
        //{
        //    var reqest = db.Requests.SingleOrDefault(r =>r.ExportNumber == model.ExportNumber);
        //    var allowed = reqest == null || reqest.Id == model.Id;
        //    return Json(allowed);
        //}

        [HttpGet]
        public IActionResult GetRequestsByFilters()
        {

            ViewBag.DeviceTypes = db.DeviceTypes.
                Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Type });
            ViewBag.Branches = db.Branchs.
                Select(b => new SelectListItem { Text = b.Name, Value = b.Id.ToString() });
            return View();
        }
    }
}
