﻿namespace RequestAndDelivery.Data.ViewModels
{
    public class DelivaryViewModel
    { 
        public string Type { get; set; }
        public string ExportNumber { get; set; }
        public string SerialNumber { get; set; }
        public string Model { get; set; }
        public bool IsNew { get; set; }
        public string  Note { get; set; }
        public string DelivaryDate { get; set; }
        public string EmployeeDeliverToMobile { get; set; }
        public string EmployeeDeliverFromMobile { get; set; }
        public int EmployeeDeliverToId { get; set; }
        public int? EmployeeDeliverFromId { get; set; }
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

    public class DeliverWithRequestDetails : FilteredDeliveriesViewModel
    {
        public string ReqDate { get; set; }
        public string ReqEmpNumber { get; set; }
        public string ReqEmpName { get; set; }
        public string ReqEmpBranch { get; set; }
        public string ReqEmpDepartment { get; set; }
    }

    }
