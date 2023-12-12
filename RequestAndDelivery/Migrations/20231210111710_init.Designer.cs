﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RequestAndDelivery.Data;

#nullable disable

namespace RequestAndDelivery.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231210111710_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("RequestAndDelivery.Data.Domain_Models.Branch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Branchs");
                });

            modelBuilder.Entity("RequestAndDelivery.Data.Domain_Models.BranchDepartment", b =>
                {
                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.HasKey("DepartmentId", "BranchId");

                    b.HasIndex("BranchId")
                        .IsUnique();

                    b.HasIndex("DepartmentId")
                        .IsUnique();

                    b.ToTable("BranchDepartments");
                });

            modelBuilder.Entity("RequestAndDelivery.Data.Domain_Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("RequestAndDelivery.Data.Domain_Models.DeviceType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DeviceTypes");
                });

            modelBuilder.Entity("RequestAndDelivery.Data.Domain_Models.Employee", b =>
                {
                    b.Property<string>("MobileNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("BranchDepartmentBranchId")
                        .HasColumnType("int");

                    b.Property<int>("BranchDepartmentDepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MobileNumber");

                    b.HasIndex("BranchDepartmentDepartmentId", "BranchDepartmentBranchId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("RequestAndDelivery.Data.Domain_Models.Request", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BranchDepartmentBranchId")
                        .HasColumnType("int");

                    b.Property<int>("BranchDepartmentDepartmentId")
                        .HasColumnType("int");

                    b.Property<int>("DeviceTypeId")
                        .HasColumnType("int");

                    b.Property<string>("EmployeeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DeviceTypeId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("BranchDepartmentDepartmentId", "BranchDepartmentBranchId");

                    b.ToTable("Request");
                });

            modelBuilder.Entity("RequestAndDelivery.Data.Domain_Models.BranchDepartment", b =>
                {
                    b.HasOne("RequestAndDelivery.Data.Domain_Models.Branch", "Branch")
                        .WithOne("Departments")
                        .HasForeignKey("RequestAndDelivery.Data.Domain_Models.BranchDepartment", "BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RequestAndDelivery.Data.Domain_Models.Department", "Department")
                        .WithOne("Branchs")
                        .HasForeignKey("RequestAndDelivery.Data.Domain_Models.BranchDepartment", "DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("RequestAndDelivery.Data.Domain_Models.Employee", b =>
                {
                    b.HasOne("RequestAndDelivery.Data.Domain_Models.BranchDepartment", "BranchDepartment")
                        .WithMany()
                        .HasForeignKey("BranchDepartmentDepartmentId", "BranchDepartmentBranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BranchDepartment");
                });

            modelBuilder.Entity("RequestAndDelivery.Data.Domain_Models.Request", b =>
                {
                    b.HasOne("RequestAndDelivery.Data.Domain_Models.DeviceType", "DeviceType")
                        .WithMany("Requests")
                        .HasForeignKey("DeviceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RequestAndDelivery.Data.Domain_Models.Employee", "Employee")
                        .WithMany("Requests")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RequestAndDelivery.Data.Domain_Models.BranchDepartment", "BranchDepartment")
                        .WithMany()
                        .HasForeignKey("BranchDepartmentDepartmentId", "BranchDepartmentBranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BranchDepartment");

                    b.Navigation("DeviceType");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("RequestAndDelivery.Data.Domain_Models.Branch", b =>
                {
                    b.Navigation("Departments")
                        .IsRequired();
                });

            modelBuilder.Entity("RequestAndDelivery.Data.Domain_Models.Department", b =>
                {
                    b.Navigation("Branchs")
                        .IsRequired();
                });

            modelBuilder.Entity("RequestAndDelivery.Data.Domain_Models.DeviceType", b =>
                {
                    b.Navigation("Requests");
                });

            modelBuilder.Entity("RequestAndDelivery.Data.Domain_Models.Employee", b =>
                {
                    b.Navigation("Requests");
                });
#pragma warning restore 612, 618
        }
    }
}
