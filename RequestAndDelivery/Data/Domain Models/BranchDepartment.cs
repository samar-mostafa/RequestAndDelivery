using System.ComponentModel.DataAnnotations;

namespace RequestAndDelivery.Data.Domain_Models
{
    public class BranchDepartment
    {
      
        public int BranchId { get; set; }
     
        public int DepartmentId { get; set; }
        public Branch Branch { get; set; }
        public Department Department { get; set; }
    }
}
