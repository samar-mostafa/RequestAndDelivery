﻿namespace RequestAndDelivery.Data.Domain_Models
{
    public class Branch
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public BranchDepartment Departments { get; set; }
    }
}
