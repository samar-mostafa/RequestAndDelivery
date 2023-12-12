using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RequestAndDelivery.Migrations
{
    public partial class editTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Delivaries_Request_RequestId",
                table: "Delivaries");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_BranchDepartments_BranchDepartmentBranchId_BranchDepartmentDepartmentId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_BranchDepartments_BranchDepartmentBranchId_BranchDepartmentDepartmentId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_DeviceTypes_DeviceTypeId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Employees_EmployeeId",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Employees_BranchDepartmentBranchId_BranchDepartmentDepartmentId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Request",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Request_BranchDepartmentBranchId_BranchDepartmentDepartmentId",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "BranchDepartmentBranchId",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "BranchDepartmentDepartmentId",
                table: "Request");

            migrationBuilder.RenameTable(
                name: "Request",
                newName: "Requests");

            migrationBuilder.RenameColumn(
                name: "BranchDepartmentDepartmentId",
                table: "Employees",
                newName: "DepartmentId");

            migrationBuilder.RenameColumn(
                name: "BranchDepartmentBranchId",
                table: "Employees",
                newName: "BranchId");

            migrationBuilder.RenameIndex(
                name: "IX_Request_EmployeeId",
                table: "Requests",
                newName: "IX_Requests_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Request_DeviceTypeId",
                table: "Requests",
                newName: "IX_Requests_DeviceTypeId");

            migrationBuilder.AddColumn<string>(
                name: "ExportNumber",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Requests",
                table: "Requests",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BranchId",
                table: "Employees",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Delivaries_Requests_RequestId",
                table: "Delivaries",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Branchs_BranchId",
                table: "Employees",
                column: "BranchId",
                principalTable: "Branchs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_DeviceTypes_DeviceTypeId",
                table: "Requests",
                column: "DeviceTypeId",
                principalTable: "DeviceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Employees_EmployeeId",
                table: "Requests",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "MobileNumber",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Delivaries_Requests_RequestId",
                table: "Delivaries");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Branchs_BranchId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_DeviceTypes_DeviceTypeId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Employees_EmployeeId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Employees_BranchId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Requests",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ExportNumber",
                table: "Requests");

            migrationBuilder.RenameTable(
                name: "Requests",
                newName: "Request");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "Employees",
                newName: "BranchDepartmentDepartmentId");

            migrationBuilder.RenameColumn(
                name: "BranchId",
                table: "Employees",
                newName: "BranchDepartmentBranchId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_EmployeeId",
                table: "Request",
                newName: "IX_Request_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_DeviceTypeId",
                table: "Request",
                newName: "IX_Request_DeviceTypeId");

            migrationBuilder.AddColumn<int>(
                name: "BranchDepartmentBranchId",
                table: "Request",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BranchDepartmentDepartmentId",
                table: "Request",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Request",
                table: "Request",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BranchDepartmentBranchId_BranchDepartmentDepartmentId",
                table: "Employees",
                columns: new[] { "BranchDepartmentBranchId", "BranchDepartmentDepartmentId" });

            migrationBuilder.CreateIndex(
                name: "IX_Request_BranchDepartmentBranchId_BranchDepartmentDepartmentId",
                table: "Request",
                columns: new[] { "BranchDepartmentBranchId", "BranchDepartmentDepartmentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Delivaries_Request_RequestId",
                table: "Delivaries",
                column: "RequestId",
                principalTable: "Request",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_BranchDepartments_BranchDepartmentBranchId_BranchDepartmentDepartmentId",
                table: "Employees",
                columns: new[] { "BranchDepartmentBranchId", "BranchDepartmentDepartmentId" },
                principalTable: "BranchDepartments",
                principalColumns: new[] { "BranchId", "DepartmentId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_BranchDepartments_BranchDepartmentBranchId_BranchDepartmentDepartmentId",
                table: "Request",
                columns: new[] { "BranchDepartmentBranchId", "BranchDepartmentDepartmentId" },
                principalTable: "BranchDepartments",
                principalColumns: new[] { "BranchId", "DepartmentId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_DeviceTypes_DeviceTypeId",
                table: "Request",
                column: "DeviceTypeId",
                principalTable: "DeviceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Employees_EmployeeId",
                table: "Request",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "MobileNumber",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
