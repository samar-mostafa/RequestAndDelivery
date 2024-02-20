using System.ComponentModel.DataAnnotations;

namespace RequestAndDelivery.Data.ViewModels
{
    public class DelivaryFormViewModel
    {
        public string DeviceType { get; set; }
        public int RequestId { get; set; }
        public string RequestEmployeeId { get; set; }
        public string RequestEmployeeName { get; set; }
        public string RequestEmployeeBranch { get; set; }
        public string RequestEmployeeDepartment { get; set; }
        public string ExportNumber { get; set; }
        [Required(ErrorMessage = "يجب ادخال سيريال الجهاز")]
        public string SerialNumber { get; set; }
        public string Model { get; set; }
     
        public DateTime DelivaryDate { get; set; }=DateTime.Now;
        public bool IsNew { get; set; }=true;
        [Required(ErrorMessage = "يجب اختيار الاداره")]
        public int EmployeeDeliverToDepartmentId { get; set; }
        [Required(ErrorMessage = "يجب اختيار الفرع")]
        public int EmployeeDeliverToBranchId { get; set; }

        public string EmployeeDeliverToName { get; set; }

        [RegularExpression("^01[0,1,2,5]{1}[0-9]{8}$", ErrorMessage = "رقم التليفون غير صحيح")]
        public string EmployeeDeliverToId { get; set; }

        public string Note { get; set; }
        public int? EmployeeDeliverFromDepartmentId { get; set; }
       
        public int? EmployeeDeliverFromBranchId { get; set; }
       
        public string EmployeeDeliverFromName { get; set; }
       
       
        [MaxLength(11, ErrorMessage = "يجب ادخال 11 رقم فقط")]
        public string EmployeeDeliverFromId { get; set; }
    }
}
