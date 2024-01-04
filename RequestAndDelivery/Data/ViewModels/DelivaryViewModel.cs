namespace RequestAndDelivery.Data.ViewModels
{
    public class DelivaryViewModel
    { 
        public string DeviceType { get; set; }
        public string ExportNumber { get; set; }
        public string SerialNumber { get; set; }
        public string Model { get; set; }
        public bool Status { get; set; }
        public string DelivaryDate { get; set; }
        public string EmpDelivarNumber { get; set; }
        public string EmpOwnerNumber { get; set; }
    }

    public class FilteredDeliveriesViewModel : DelivaryViewModel
    {
        public string EmpDelivarName { get; set; }
        public string BranchDelivar { get; set; }
        public string DepartmentDelivar { get; set; }
        public string EmpOwnerName { get; set; }
        public string BranchOwner { get; set; }
        public string DepartmentOwner { get; set; }

    }
}
