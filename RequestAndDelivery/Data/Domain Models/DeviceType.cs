namespace RequestAndDelivery.Data.Domain_Models
{
    public class DeviceType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public ICollection<Request> Requests { get; set; }=new List<Request>();
    }
}
