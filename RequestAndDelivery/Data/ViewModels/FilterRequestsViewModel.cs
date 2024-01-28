using System.ComponentModel.DataAnnotations;

namespace RequestAndDelivery.Data.ViewModels
{
    public class GetRequestToDeliverViewModel
    {
        public string ExportNumber { get; set; }
        public int? DeviceTypeId { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int? BranchId { get; set; }
        public int? DepartmentId { get; set; }
        public DateTime? DateFrom { get; set; }
    }
    public class FilterRequestsViewModel: GetRequestToDeliverViewModel
    {
        public DateTime? DateTo { get; set; }
      
          
        public bool IsDeliverd { get; set; } 
       
    }
}
