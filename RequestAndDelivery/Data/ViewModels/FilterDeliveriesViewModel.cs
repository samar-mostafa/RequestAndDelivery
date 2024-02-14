using Microsoft.AspNetCore.Mvc;

namespace RequestAndDelivery.Data.ViewModels
{
    public class FilterDeliveriesViewModel
    {
        public int? DeviceTypeId { get; set; }
        public string ExportNumber { get; set; }
        public string SerialNumber { get; set; }
        public string Model { get; set; }
        public string IsNew { get; set; }
        [Remote("ValidateDateGreaterThan", "Requests", AdditionalFields = "DateFrom", ErrorMessage = "التاريخ غير صحيح...يجب ادخال قيمه اكبر من التاريخ من")]
        public DateTime? DateTo { get; set; }

        public DateTime? DateFrom { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int? BranchId { get; set; }
        public int? DepartmentId { get; set; }
       
    }
}
