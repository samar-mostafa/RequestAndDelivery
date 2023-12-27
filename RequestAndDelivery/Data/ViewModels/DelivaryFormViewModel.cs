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
        [Required(ErrorMessage = "يجب ادخال الموديل")]
        public string Model { get; set; }
     
        public DateTime DelivaryDate { get; set; }=DateTime.Now;
        public bool IsNew { get; set; }=true;
        [Required(ErrorMessage = "يجب اختيار الاداره")]
        public int EmployeeDeliverToDepartmentId { get; set; }
        [Required(ErrorMessage = "يجب اختيار الفرع")]
        public int EmployeeDeliverToBranchId { get; set; }
        [Required(ErrorMessage = "يجب ادخال اسم الموظف")]
        public string EmployeeDeliverToName { get; set; }
        [Required(ErrorMessage = "يجب ادخال رقم تليفون الموظف")]
        [MinLength(11, ErrorMessage = "يجب ادخال 11 رقم ")]
        [MaxLength(11, ErrorMessage = "يجب ادخال 11 رقم فقط")]
        public string EmployeeDeliverToId { get; set; }

       
        public int? EmployeeDeliverFromDepartmentId { get; set; }
       
        public int? EmployeeDeliverFromBranchId { get; set; }
       
        public string EmployeeDeliverFromName { get; set; }
       
       
        [MaxLength(11, ErrorMessage = "يجب ادخال 11 رقم فقط")]
        public string EmployeeDeliverFromId { get; set; }
    }
}
