namespace RequestAndDelivery.Data.Domain_Models
{
    public class Delivary
    {
        public int Id { get; set; }
        public DateTime DelivaryDate { get; set; }
        public string DeviceId { get; set; }
        public Device Device { get; set; }
        public int RequestId { get; set; }
        public Request Request { get; set; }
    }
}              
