using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RequestAndDelivery.Data;
using RequestAndDelivery.Data.Domain_Models;
using RequestAndDelivery.Data.ViewModels;

namespace RequestAndDelivery.Controllers
{
    public class DelivariesController : Controller
    {
        private readonly ApplicationDbContext db;

        public DelivariesController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            var data = db.Delivaries.Include(d => d.Request).Include(d => d.Device).Select(d => new DelivaryViewModel
            {
                DeviceType = db.DeviceTypes.Where(dt => dt.Id == d.Request.DeviceTypeId)
                .Select(dt => dt.Type).SingleOrDefault(),
                DelivaryDate = d.DelivaryDate.ToShortDateString(),
                SerialNumber = d.Device.SerialNumber,
                Model = d.Device.Model,
                Status = d.Device.IsNew,
                ExportNumber = d.Request.ExportNumber,
                EmpDelivarNumber = d.Device.EmployeeDeliverToId,
                EmpOwnerNumber = d.Device.EmployeeDeliverToId
            }).ToList();
            return View(data);
        }

        public IActionResult Create(DelivaryFormViewModel mdl)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Branches = db.Branchs.Select(b => new
                SelectListItem
                { Text = b.Name, Value = b.Id.ToString() });
                return View("Form", mdl);
            }
            if (!db.Employees.Any(e => e.MobileNumber == mdl.EmployeeDeliverToId))
            {
                var employee = new Employee
                {
                    MobileNumber = mdl.EmployeeDeliverToId,
                    Name = mdl.EmployeeDeliverToName,
                    BranchId = mdl.EmployeeDeliverToBranchId,
                    DepartmentId = mdl.EmployeeDeliverToDepartmentId
                };

                db.Employees.Add(employee);
            }

            if (!mdl.IsNew)
            {
                if (!db.Employees.Any(e => e.MobileNumber == mdl.EmployeeDeliverFromId))
                {
                    var employee = new Employee
                    {
                        MobileNumber = mdl.EmployeeDeliverFromId,
                        Name = mdl.EmployeeDeliverFromName,
                        BranchId = (int)mdl.EmployeeDeliverFromBranchId,
                        DepartmentId = (int)mdl.EmployeeDeliverFromDepartmentId
                    };

                    db.Employees.Add(employee);
                }
            }
            var devic = new Device
            {
                SerialNumber = mdl.SerialNumber,
                Model = mdl.Model,
                IsNew = mdl.IsNew,
                EmployeeDeliverToId = mdl.EmployeeDeliverToId,
                EmployeeDeliverFromId = mdl.EmployeeDeliverFromId

            };
            db.Devices.Add(devic);

            var entity = new Delivary
            {
                RequestId = mdl.RequestId,
                DelivaryDate = mdl.DelivaryDate,
                DeviceId = devic.SerialNumber,
            };
            db.Delivaries.Add(entity);
            var req = db.Requests.SingleOrDefault(r => r.Id == mdl.RequestId);
            req.IsDeliverd = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DeviceDeliver(int id)
        {
            var req = db.Requests.Include(r => r.DeviceType).Include(r => r.Employee).SingleOrDefault(r => r.Id == id);
            var mdl = new DelivaryFormViewModel
            {
                RequestId = req.Id,
                DeviceType = req.DeviceType.Type,
                ExportNumber = req.ExportNumber,
                RequestEmployeeId = req.EmployeeId,
                RequestEmployeeName = req.Employee.Name,
                RequestEmployeeBranch = db.Branchs.Where(b => b.Id == req.Employee.BranchId).Select(b => b.Name).SingleOrDefault(),
                RequestEmployeeDepartment = db.Departments.Where(b => b.Id == req.Employee.DepartmentId).Select(b => b.Name).SingleOrDefault(),
            };
            ViewBag.Branches = db.Branchs.Select(b => new SelectListItem { Text = b.Name, Value = b.Id.ToString() });
            return View("Form", mdl);
        }

        [HttpGet]
        public IActionResult GetDeliveriesByFilters()
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
        public IActionResult GetDeliveriesByFilters(FilterDeliveriesViewModel mdl)
        {
            var entities = db.Delivaries.Include(d => d.Device).Include(d => d.Request).ThenInclude(r => r.Employee).
                Where(d => (d.Request.DeviceTypeId == mdl.DeviceTypeId || mdl.DeviceTypeId == null) &&
                (d.Request.ExportNumber == mdl.ExportNumber || mdl.ExportNumber == null) &&
                (d.DeviceId == mdl.SerialNumber || mdl.SerialNumber == null) &&
                (d.Device.Model == mdl.Model || mdl.Model == null) &&
                (d.Device.IsNew == mdl.IsNew) &&
                (d.DelivaryDate >= mdl.DateFrom || mdl.DateFrom == null) &&
                (d.DelivaryDate <= mdl.DateTo || mdl.DateFrom == null) &&
                (d.Device.EmployeeDeliverToId == mdl.EmployeeId ||
                d.Device.EmployeeDeliverFromId == mdl.EmployeeId || mdl.EmployeeId == null) &&
                (d.Request.Employee.BranchId == mdl.BranchId || mdl.BranchId == null) &&
                (d.Request.Employee.DepartmentId == mdl.DepartmentId || mdl.DepartmentId == null) &&
                (d.Request.Employee.Name == mdl.EmployeeName || mdl.EmployeeName == null)
                ).Select(d => new FilteredDeliveriesViewModel
                {
                    DeviceType = db.DeviceTypes.Where(dt => dt.Id == d.Request.DeviceTypeId)
                .Select(dt => dt.Type).SingleOrDefault(),
                    DelivaryDate = d.DelivaryDate.ToShortDateString(),
                    SerialNumber = d.Device.SerialNumber,
                    Model = d.Device.Model,
                    Status = d.Device.IsNew,
                    ExportNumber = d.Request.ExportNumber,
                    EmpDelivarNumber = d.Device.EmployeeDeliverToId,
                    EmpOwnerNumber = d.Device.EmployeeDeliverFromId,
                    BranchDelivar = db.Employees.Include(e => e.Branch).Where(e => e.MobileNumber == d.Device.EmployeeDeliverToId).Select(e => e.Branch.Name).SingleOrDefault(),
                    DepartmentDelivar = db.Employees.Include(e => e.Department).Where(e => e.MobileNumber == d.Device.EmployeeDeliverToId).Select(e => e.Department.Name).SingleOrDefault(),
                    BranchOwner = db.Employees.Include(e => e.Branch).Where(e => e.MobileNumber == d.Device.EmployeeDeliverFromId).Select(e => e.Branch.Name).SingleOrDefault(),
                    DepartmentOwner = db.Employees.Include(e => e.Department).Where(e => e.MobileNumber == d.Device.EmployeeDeliverFromId).Select(e => e.Department.Name).SingleOrDefault(),
                    EmpDelivarName = db.Employees.Where(e => e.MobileNumber == d.Device.EmployeeDeliverToId).Select(e => e.Name).SingleOrDefault(),
                    EmpOwnerName = db.Employees.Where(e => e.MobileNumber == d.Device.EmployeeDeliverFromId).Select(e => e.Name).SingleOrDefault(),

                }).ToList();

            var pageSize = int.Parse(Request.Form["length"]);
            var skip = int.Parse(Request.Form["start"]);
          
          //  var sortingValue = Request.Form[string.Concat("columns[", Request.Form["order[0][column]"], "][data]")];
          //  var sortDirection = Request.Form["order[0][dir]"];
           
           
          //// entities.OrderBy(string.Concat(sortingValue, " ", sortDirection));

          
            var data = entities.Skip(skip).Take(pageSize).ToList();

            var recordsTotal = entities.Count();
            var jsonData = new { recordsFiltered = recordsTotal, recordsTotal, data };
            return Ok(jsonData);
        }
    }
}

