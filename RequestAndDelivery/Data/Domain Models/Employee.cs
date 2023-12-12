using System.ComponentModel.DataAnnotations;

namespace RequestAndDelivery.Data.Domain_Models
{
    public class Employee
    {
        [Key]
        
        public string MobileNumber { get; set; }
        public string Name { get; set; }
        public int BranchId { get; set; }
        public int DepartmentId { get; set; }

        public Department Department { get; set; }
        public Branch Branch { get; set; }
        public ICollection<Request> Requests { get; set; } = new List<Request>();
    }
}
