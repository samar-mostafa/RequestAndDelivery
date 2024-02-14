namespace RequestAndDelivery.Data.ViewModels
{
    public class RequestViewModel
    {
        public int Id { get; set; }
        public string DeviceType { get; set; }
        public string ExportNumber { get; set; }
        public string RequestDate { get; set; }
        public string EmpNumber { get; set; }
        public bool IsDeliverd { get; set; }
        public string Note { get; set; }
    }

    public class FilteredRequestViewModel : RequestViewModel
    {
        public string EmpName { get; set; }
        public string Branch { get; set; }
        public string Department { get; set; }
    }
}
