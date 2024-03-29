﻿using AutoMapper;
using RequestAndDelivery.Data.Domain_Models;
using RequestAndDelivery.Data.ViewModels;
using System.Linq;


namespace RequestAndDelivery.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Delivary, FilteredDeliveriesViewModel>()
                .ForMember(des => des.Type, op => op.MapFrom(src => src.Request.DeviceType.Type))
                .ForMember(des => des.ExportNumber, op => op.MapFrom(src => src.Request.ExportNumber))
               .ForMember(des => des.SerialNumber, op => op.MapFrom(src => src.Device.SerialNumber))
               .ForMember(des => des.IsNew, op => op.MapFrom(src => src.Device.IsNew))
               .ForMember(des => des.Model, op => op.MapFrom(src => src.Device.Model))
               .ForMember(des => des.DelivaryDate, op => op.MapFrom(src => src.DelivaryDate.ToShortDateString()))
               .ForMember(des => des.EmployeeDeliverToId, op => op.MapFrom(src => src.Device.EmployeeDeliverToId))
               .ForMember(des => des.EmployeeDeliverToMobile, op => op.MapFrom(src => src.Device.EmployeeDeliverTo.MobileNumber))               
               .ForMember(des => des.EmployeeDeliverFromId, op => op.MapFrom(src => src.Device.EmployeeDeliverFromId))
                .ForMember(des => des.EmployeeDeliverFromMobile, op => op.MapFrom(src => src.Device.EmployeeDeliverFrom.MobileNumber))
              .ForMember(des => des.BranchDelivar, op => op.MapFrom(src => src.Device.EmployeeDeliverTo.Branch.Name))
              .ForMember(des => des.BranchOwner, op => op.MapFrom(src => src.Device.EmployeeDeliverFrom.Branch.Name))
               .ForMember(des => des.DepartmentDelivar, op => op.MapFrom(src => src.Device.EmployeeDeliverTo.Department.Name))
              .ForMember(des => des.DepartmentOwner, op => op.MapFrom(src => src.Device.EmployeeDeliverFrom.Department.Name))
              .ForMember(des => des.EmpOwnerName, op => op.MapFrom(src => src.Device.EmployeeDeliverFrom.Name))
              .ForMember(des => des.EmpDelivarName, op => op.MapFrom(src => src.Device.EmployeeDeliverTo.Name))
                ;


            CreateMap<Delivary, DeliverWithRequestDetails>()
              .ForMember(des => des.Type, op => op.MapFrom(src => src.Request.DeviceType.Type))
              .ForMember(des => des.ExportNumber, op => op.MapFrom(src => src.Request.ExportNumber))
              .ForMember(des => des.ReqDate, op => op.MapFrom(src => src.Request.RequestDate.ToShortDateString()))
              .ForMember(des => des.ReqEmpNumber, op => op.MapFrom(src => src.Request.EmployeeId))
              .ForMember(des => des.ReqEmpName, op => op.MapFrom(src => src.Request.Employee.Name))
              .ForMember(des => des.ReqEmpBranch, op => op.MapFrom(src => src.Request.Employee.Branch.Name))
              .ForMember(des => des.ReqEmpDepartment, op => op.MapFrom(src => src.Request.Employee.Department.Name))
              .ForMember(des => des.SerialNumber, op => op.MapFrom(src => src.Device.SerialNumber))
              .ForMember(des => des.IsNew, op => op.MapFrom(src => src.Device.IsNew))
              .ForMember(des => des.Model, op => op.MapFrom(src => src.Device.Model))
              .ForMember(des => des.DelivaryDate, op => op.MapFrom(src => src.DelivaryDate.ToShortDateString()))
              .ForMember(des => des.EmployeeDeliverToId, op => op.MapFrom(src => src.Device.EmployeeDeliverToId))
              .ForMember(des => des.EmployeeDeliverFromId, op => op.MapFrom(src => src.Device.EmployeeDeliverFromId))
              .ForMember(des => des.BranchDelivar, op => op.MapFrom(src => src.Device.EmployeeDeliverTo.Branch.Name))
              .ForMember(des => des.BranchOwner, op => op.MapFrom(src => src.Device.EmployeeDeliverFrom.Branch.Name))
              .ForMember(des => des.DepartmentDelivar, op => op.MapFrom(src => src.Device.EmployeeDeliverTo.Department.Name))
              .ForMember(des => des.DepartmentOwner, op => op.MapFrom(src => src.Device.EmployeeDeliverFrom.Department.Name))
              .ForMember(des => des.EmpOwnerName, op => op.MapFrom(src => src.Device.EmployeeDeliverFrom.Name))
              .ForMember(des => des.EmpDelivarName, op => op.MapFrom(src => src.Device.EmployeeDeliverTo.Name))
               ;
        }
    }
}
