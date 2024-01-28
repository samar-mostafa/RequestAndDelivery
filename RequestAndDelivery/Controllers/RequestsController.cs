using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RequestAndDelivery.Data;
using RequestAndDelivery.Data.Domain_Models;
using RequestAndDelivery.Data.ViewModels;
using System.Text.Json;
using System.Linq.Dynamic.Core;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;

namespace RequestAndDelivery.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RequestsController : Controller
    {
        private readonly ApplicationDbContext db;

        public RequestsController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            var requests = db.Requests;
            @ViewBag.All = requests.Count();
            @ViewBag.Delivered=requests.Count(q=>q.IsDeliverd);
            @ViewBag.NotDelivered= requests.Count(q => !q.IsDeliverd);
            return View();
        }


      

        [HttpPost]
        public IActionResult GetRequests(bool? val)
        {
            var requests = db.Requests.Include(r => r.DeviceType).Where(r => r.IsDeliverd == val || val == null)
            .Select(r => new RequestViewModel
            {
                       Id = r.Id,
                       DeviceType = r.DeviceType.Type,
                       ExportNumber = r.ExportNumber,
                       IsDeliverd = r.IsDeliverd,
                       RequestDate = r.RequestDate.ToShortDateString(),
                       EmpNumber = r.EmployeeId,

            });
          
            var pageSize = int.Parse(Request.Form["length"]);
            var skip = int.Parse(Request.Form["start"]);
            var searchValue = Request.Form["search[value]"];
            var sortColumnIndex= Request.Form["order[0][column]"];
            var sortingColumn = Request.Form[$"columns[{sortColumnIndex}][name]"]; ;
            var sortDirection = Request.Form["order[0][dir]"];
            var searchValueNumber = 0;
            var r = int.TryParse(searchValue, out searchValueNumber);
            requests.OrderBy($"{sortingColumn} {sortDirection}");

            var reqs = requests.AsEnumerable().Where(q => string.IsNullOrEmpty(searchValue) ? true :
                (q.DeviceType.ToLower().Contains(searchValue.ToString().ToLower()) ||
               (q.ExportNumber!=null&& q.ExportNumber.Contains(searchValue.ToString()))||
                q.RequestDate.Contains(searchValue.ToString()) ||
                q.EmpNumber.Contains(searchValue.ToString())));

            var data = reqs.Skip(skip).Take(pageSize).ToList();
            var recordsTotal = requests.Count();
            var jsonData = new { recordsFiltered = recordsTotal, recordsTotal, data };
            return Ok(jsonData);
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
            var emp = db.Employees.Include(e=>e.Branch).Include(e=>e.Department)
                .Where(e => e.MobileNumber == id)
                .Select(e => new EmployeeDataViewModel
                {
                    MobilePhone = e.MobileNumber,
                    Name = e.Name,
                    Branche = e.Branch.Name,
                    Department =e.Department.Name
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
                        Value = b.Id.ToString()
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
        //    var reqest = db.Requests.SingleOrDefault(r => r.ExportNumber == model.ExportNumber);
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
            ViewBag.Departments = db.Departments.
               Select(b => new SelectListItem { Text = b.Name, Value = b.Id.ToString() });
            return View();
        }

        [HttpPost]
        public IActionResult GetRequestsByFilters(FilterRequestsViewModel mdl)
        {
            var entities = db.Requests.Include(r => r.Employee).Include(r => r.DeviceType).
                Where(r => r.IsDeliverd == Convert.ToBoolean( mdl.IsDeliverd) &&
            (r.DeviceTypeId == mdl.DeviceTypeId || mdl.DeviceTypeId == null) &&
            (r.EmployeeId == mdl.EmployeeId || mdl.EmployeeId == null) &&
            (r.RequestDate >= mdl.DateFrom || mdl.DateFrom == null) &&
            (r.RequestDate <= mdl.DateTo || mdl.DateFrom == null) &&
            (r.ExportNumber == mdl.ExportNumber || mdl.ExportNumber == null) &&
            (r.Employee.Name == mdl.EmployeeName || mdl.EmployeeName == null) &&
            (r.Employee.BranchId == mdl.BranchId || mdl.BranchId == null) &&
            (r.Employee.DepartmentId == mdl.DepartmentId || mdl.DepartmentId == null)
            ).Select(r => new FilteredRequestViewModel
            {   

                DeviceType = r.DeviceType.Type,
                ExportNumber = r.ExportNumber,
                IsDeliverd = r.IsDeliverd,
                RequestDate = r.RequestDate.ToShortDateString(),
                EmpNumber = r.EmployeeId,
                EmpName = r.Employee.Name,
                Branch = db.Employees.Include(bd=>bd.Branch).Where(e=>e.MobileNumber==r.EmployeeId).Select(e=>e.Branch.Name).SingleOrDefault(),
                Department = db.Employees.Include(bd => bd.Department).Where(e => e.MobileNumber == r.EmployeeId).Select(e => e.Department.Name).SingleOrDefault(),
            });
            var pageSize = int.Parse(Request.Form["length"]);
            var skip = int.Parse(Request.Form["start"]);
          
            var sortingValue = Request.Form[string.Concat("columns[", Request.Form["order[0][column]"], "][data]")];
            var sortDirection = Request.Form["order[0][dir]"];
           
         
            entities.OrderBy(string.Concat(sortingValue, " ", sortDirection));
           
            var data = entities.Skip(skip).Take(pageSize).ToList();

            var recordsTotal = entities.Count();
            var jsonData = new { recordsFiltered = recordsTotal, recordsTotal, data };
            return Ok(jsonData);
        }

     


}


   
}
