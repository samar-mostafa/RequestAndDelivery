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
                DelivaryDate=d.DelivaryDate.ToShortDateString(),
                SerialNumber=d.Device.SerialNumber,
                Model=d.Device.Model,
                Status=d.Device.IsNew,
                ExportNumber=d.Request.ExportNumber,
                EmpDelivarNumber=d.Device.EmployeeDeliverToId,
                EmpOwnerNumber=d.Device.EmployeeDeliverToId
            }).ToList();
            return View(data);
        }

        public IActionResult Create(DelivaryFormViewModel mdl)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Branches = db.Branchs.Select(b => new 
                SelectListItem { Text = b.Name, Value = b.Id.ToString() });
                return View("Form", mdl);
            }
            if (!db.Employees.Any(e => e.MobileNumber == mdl.EmployeeDeliverToId))
            {
                var employee = new Employee
                {
                    MobileNumber = mdl.EmployeeDeliverToId,
                    Name = mdl.EmployeeDeliverToName,
                    BranchId =mdl.EmployeeDeliverToBranchId,
                    DepartmentId = mdl.EmployeeDeliverToDepartmentId
                };

                db.Employees.Add(employee);
            }

            if( !mdl.IsNew)
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
            var req = db.Requests.Include(r=>r.DeviceType).Include(r=>r.Employee).SingleOrDefault(r=>r.Id==id);
            var mdl = new DelivaryFormViewModel
            {
                RequestId=req.Id,
                DeviceType = req.DeviceType.Type,
                ExportNumber = req.ExportNumber,
                RequestEmployeeId = req.EmployeeId,
                RequestEmployeeName = req.Employee.Name,
                RequestEmployeeBranch = db.Branchs.Where(b => b.Id == req.Employee.BranchId).Select(b => b.Name).SingleOrDefault(),
                RequestEmployeeDepartment= db.Departments.Where(b => b.Id == req.Employee.DepartmentId).Select(b => b.Name).SingleOrDefault(),
            };
            ViewBag.Branches = db.Branchs.Select(b => new SelectListItem { Text = b.Name, Value = b.Id.ToString() });
            return View("Form",mdl);
        }
    }
}
