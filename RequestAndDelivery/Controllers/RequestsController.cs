using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RequestAndDelivery.Data;
using RequestAndDelivery.Data.Domain_Models;
using RequestAndDelivery.Data.ViewModels;

namespace RequestAndDelivery.Controllers
{
    public class RequestsController : Controller
    {
        private readonly ApplicationDbContext db;

        public RequestsController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index(bool? val)
        {
            var requests = db.Requests.Include(r => r.DeviceType).Where(r => r.IsDeliverd == val || val==null).Select(r=> new RequestViewModel
            {
                Id= r.Id,
                DeviceType=r.DeviceType.Type,
                ExportNumber=r.ExportNumber,
                IsDeliverd=r.IsDeliverd,
                RequestDate = r.RequestDate.ToShortDateString(),
                EmpNumber=r.EmployeeId
            }).ToList();

            if(val == null)              
                return View(requests);
            else
                return Json(requests);
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

        public IActionResult AllowItem(RequestFormViewModel model)
        {
            var reqest = db.Requests.SingleOrDefault(r =>r.ExportNumber == model.ExportNumber);
            var allowed = reqest == null || reqest.Id == model.Id;
            return Json(allowed);
        }
    }
}
