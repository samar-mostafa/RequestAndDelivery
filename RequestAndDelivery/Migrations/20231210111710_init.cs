using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RequestAndDelivery.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Branchs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branchs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BranchDepartments",
                columns: table => new
                {
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchDepartments", x => new { x.DepartmentId, x.BranchId });
                    table.ForeignKey(
                        name: "FK_BranchDepartments_Branchs_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branchs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BranchDepartments_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    MobileNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchDepartmentDepartmentId = table.Column<int>(type: "int", nullable: false),
                    BranchDepartmentBranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.MobileNumber);
                    table.ForeignKey(
                        name: "FK_Employees_BranchDepartments_BranchDepartmentDepartmentId_BranchDepartmentBranchId",
                        columns: x => new { x.BranchDepartmentDepartmentId, x.BranchDepartmentBranchId },
                        principalTable: "BranchDepartments",
                        principalColumns: new[] { "DepartmentId", "BranchId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Request",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchDepartmentDepartmentId = table.Column<int>(type: "int", nullable: false),
                    BranchDepartmentBranchId = table.Column<int>(type: "int", nullable: false),
                    DeviceTypeId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Request_BranchDepartments_BranchDepartmentDepartmentId_BranchDepartmentBranchId",
                        columns: x => new { x.BranchDepartmentDepartmentId, x.BranchDepartmentBranchId },
                        principalTable: "BranchDepartments",
                        principalColumns: new[] { "DepartmentId", "BranchId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Request_DeviceTypes_DeviceTypeId",
                        column: x => x.DeviceTypeId,
                        principalTable: "DeviceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Request_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "MobileNumber",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BranchDepartments_BranchId",
                table: "BranchDepartments",
                column: "BranchId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BranchDepartments_DepartmentId",
                table: "BranchDepartments",
                column: "DepartmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BranchDepartmentDepartmentId_BranchDepartmentBranchId",
                table: "Employees",
                columns: new[] { "BranchDepartmentDepartmentId", "BranchDepartmentBranchId" });

            migrationBuilder.CreateIndex(
                name: "IX_Request_BranchDepartmentDepartmentId_BranchDepartmentBranchId",
                table: "Request",
                columns: new[] { "BranchDepartmentDepartmentId", "BranchDepartmentBranchId" });

            migrationBuilder.CreateIndex(
                name: "IX_Request_DeviceTypeId",
                table: "Request",
                column: "DeviceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_EmployeeId",
                table: "Request",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Request");

            migrationBuilder.DropTable(
                name: "DeviceTypes");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "BranchDepartments");

            migrationBuilder.DropTable(
                name: "Branchs");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
