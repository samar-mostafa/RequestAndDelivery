using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RequestAndDelivery.Data;
using RequestAndDelivery.Data.Domain_Models;
using RequestAndDelivery.Data.ViewModels;
using System.Linq.Dynamic.Core;

namespace RequestAndDelivery.Controllers
{
    public class DelivariesController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public DelivariesController(ApplicationDbContext db,IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            var data = db.Delivaries.Include(d => d.Request).Include(d => d.Device).Select(d => new DelivaryViewModel
            {
                Type = db.DeviceTypes.Where(dt => dt.Id == d.Request.DeviceTypeId)
                .Select(dt => dt.Type).SingleOrDefault(),
                DelivaryDate = d.DelivaryDate.ToShortDateString(),
                SerialNumber = d.Device.SerialNumber,
                Model = d.Device.Model,
                IsNew = d.Device.IsNew,
                ExportNumber = d.Request.ExportNumber,
                EmployeeDeliverToId = d.Device.EmployeeDeliverToId
               
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
        public IActionResult DeliveriesByFilters(FilterDeliveriesViewModel mdl)
        {
            var entities = db.Delivaries.Include(d => d.Device).ThenInclude(d=>d.EmployeeDeliverTo).ThenInclude(e=>  e.Branch)
                .Include(d => d.Device).ThenInclude(d => d.EmployeeDeliverTo).ThenInclude(e => e.Department)
                .Include(d => d.Device).ThenInclude(d => d.EmployeeDeliverFrom).ThenInclude(e => e.Department)
                .Include(d => d.Device).ThenInclude(d => d.EmployeeDeliverFrom).ThenInclude(e => e.Branch)
                .Include(d => d.Request).ThenInclude(r => r.DeviceType)
              
                .Where(d => (d.Request.DeviceTypeId == mdl.DeviceTypeId || mdl.DeviceTypeId == null) &&
                (d.Request.ExportNumber == mdl.ExportNumber || mdl.ExportNumber == null) &&
                (d.DeviceId == mdl.SerialNumber || mdl.SerialNumber == null) &&
                (d.Device.Model == mdl.Model || mdl.Model == null) &&
                (d.Device.IsNew.ToString() == mdl.IsNew || mdl.IsNew == null) &&
                (d.DelivaryDate >= mdl.DateFrom || mdl.DateFrom == null) &&
                (d.DelivaryDate <= mdl.DateTo || mdl.DateFrom == null) &&
                (d.Device.EmployeeDeliverToId == mdl.EmployeeId ||
                d.Device.EmployeeDeliverFromId == mdl.EmployeeId || mdl.EmployeeId == null) &&
                (d.Request.Employee.BranchId == mdl.BranchId || mdl.BranchId == null) &&
                (d.Request.Employee.DepartmentId == mdl.DepartmentId || mdl.DepartmentId == null) &&
                (d.Request.Employee.Name == mdl.EmployeeName || mdl.EmployeeName == null));
                //).Select(d=>new FilteredDeliveriesViewModel
                //{
                //    Model=d.Device.Model,
                //    SerialNumber=d.Device.SerialNumber,
                //    Type=d.Request.DeviceType.Type
                //} ).ToList();
            var mappingData = mapper.Map<IEnumerable<FilteredDeliveriesViewModel>>(entities).AsQueryable();
            
            var pageSize = int.Parse(Request.Form["length"]);
            var skip = int.Parse(Request.Form["start"]);

            var sortingValue = Request.Form[string.Concat("columns[", Request.Form["order[0][column]"], "][data]")];
            var sortDirection = Request.Form["order[0][dir]"];


            mappingData.OrderBy(string.Concat(sortingValue, " ", sortDirection));


            var data = mappingData.Skip(skip).Take(pageSize).ToList();

            var recordsTotal = entities.Count();
            var jsonData = new { recordsFiltered = recordsTotal, recordsTotal, data };
            return Ok(jsonData);
        }
    }
}

