using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace RequestAndDelivery.Data.Domain_Models
{
   
    public class Request
    {
        public int Id { get; set; }
        public int DeviceTypeId { get; set; }
        public int EmployeeId { get; set; }
        public string ExportNumber { get; set; }
        public DateTime RequestDate { get; set; }
        public string Note { get; set; }
        public bool IsDeliverd { get; set; }
        public Employee Employee { get; set; }
        public DeviceType DeviceType { get; set; }
    }
}
