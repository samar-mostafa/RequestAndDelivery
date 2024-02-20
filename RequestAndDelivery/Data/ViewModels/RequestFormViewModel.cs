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
        
        public string EmployeeName { get; set; }      
        [RegularExpression("^01[0,1,2,5]{1}[0-9]{8}$",ErrorMessage ="رقم التليفون غير صحيح")]
        public string EmployeeId { get; set; }
        //[Required(ErrorMessage = "يجب ادخال رقم الصادر")]
        //[Remote("AllowItem",null,AdditionalFields =nameof(Id),ErrorMessage ="رقم الصادر موجود بالفعل")]
        public string ExportNumber { get; set; }
        [Required(ErrorMessage = "يجب ادخال تاريخ الطلب")]
        public DateTime RequestDate { get; set; }

        public string Note { get; set; }
    }
}
