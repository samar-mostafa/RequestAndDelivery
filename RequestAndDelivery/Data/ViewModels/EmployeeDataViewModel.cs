using Microsoft.AspNetCore.Mvc.Rendering;

namespace RequestAndDelivery.Data.ViewModels
{
    public class EmployeeDataViewModel
    {
        public string MobilePhone { get; set; } 
        public string Name { get; set; }
        public string Branche { get; set; }
        public string Department{ get; set; }
        public SelectListItem  BranchData { get; set; }
        public SelectListItem DepartmentData { get; set; }
    }
}
