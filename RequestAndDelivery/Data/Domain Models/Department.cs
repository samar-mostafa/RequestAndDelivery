namespace RequestAndDelivery.Data.Domain_Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public BranchDepartment Branchs { get; set; }
    }
}
