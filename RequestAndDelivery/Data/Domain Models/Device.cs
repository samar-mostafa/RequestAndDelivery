using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RequestAndDelivery.Data.Domain_Models
{
    public class Device
    {
        [Key]
        public string SerialNumber { get; set; }
        public string Model { get; set; }
        public bool IsNew { get; set; }
        public string EmployeeDeliverToId{ get; set; }

        [ForeignKey(nameof(EmployeeDeliverToId))]
        public Employee EmployeeDeliverTo { get; set; }
        public string? EmployeeDeliverFromId { get; set; }

        [ForeignKey(nameof(EmployeeDeliverFromId))]
        public Employee? EmployeeDeliverFrom { get; set; }
    }
}
