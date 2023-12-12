using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RequestAndDelivery.Data.ViewModels
{
    public class RequestFormViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="يجب اختيار الاداره")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "يجب اختيار الفرع")]
        public int BranchId { get; set; }
        [Required(ErrorMessage = "يجب اختيار نوع الجهاز")]
        public int DeviceTypeId { get; set; }
        [Required(ErrorMessage = "يجب ادخال اسم الموظف")]
        public string EmployeeName { get; set; }
        [Required(ErrorMessage = "يجب ادخال رقم تليفون الموظف")]
        [MinLength(11 ,ErrorMessage ="يجب ادخال 11 رقم ")]
        [MaxLength(11, ErrorMessage = "يجب ادخال 11 رقم فقط")]
        public string EmployeeId { get; set; }
        [Required(ErrorMessage = "يجب ادخال رقم الصادر")]
        [Remote("AllowItem",null,AdditionalFields =nameof(Id),ErrorMessage ="رقم الصادر موجود بالفعل")]
        public string ExportNumber { get; set; }
        [Required(ErrorMessage = "يجب ادخال تاريخ الطلب")]
        public DateTime RequestDate { get; set; }
    }
}
