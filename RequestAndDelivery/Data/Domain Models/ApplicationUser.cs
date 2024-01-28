using Microsoft.AspNetCore.Identity;

namespace RequestAndDelivery.Data.Domain_Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedOnId { get; set; }
        public string LastUpdatedOnId { get; set; }
        public DateTime CreatedOn { get; set; }=DateTime.Now;
        public DateTime LastUpdatedOn { get; set; }
    }
}
