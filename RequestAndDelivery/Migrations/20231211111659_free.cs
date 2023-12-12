using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RequestAndDelivery.Migrations
{
    public partial class free : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_BranchDepartments_BranchDepartmentDepartmentId_BranchDepartmentBranchId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_BranchDepartments_BranchDepartmentDepartmentId_BranchDepartmentBranchId",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Request_BranchDepartmentDepartmentId_BranchDepartmentBranchId",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Employees_BranchDepartmentDepartmentId_BranchDepartmentBranchId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BranchDepartments",
                table: "BranchDepartments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BranchDepartments",
                table: "BranchDepartments",
                columns: new[] { "BranchId", "DepartmentId" });

            migrationBuilder.CreateIndex(
                name: "IX_Request_BranchDepartmentBranchId_BranchDepartmentDepartmentId",
                table: "Request",
                columns: new[] { "BranchDepartmentBranchId", "BranchDepartmentDepartmentId" });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BranchDepartmentBranchId_BranchDepartmentDepartmentId",
                table: "Employees",
                columns: new[] { "BranchDepartmentBranchId", "BranchDepartmentDepartmentId" });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_BranchDepartments_BranchDepartmentBranchId_BranchDepartmentDepartmentId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_BranchDepartments_BranchDepartmentBranchId_BranchDepartmentDepartmentId",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Request_BranchDepartmentBranchId_BranchDepartmentDepartmentId",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Employees_BranchDepartmentBranchId_BranchDepartmentDepartmentId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BranchDepartments",
                table: "BranchDepartments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BranchDepartments",
                table: "BranchDepartments",
                columns: new[] { "DepartmentId", "BranchId" });

            migrationBuilder.CreateIndex(
                name: "IX_Request_BranchDepartmentDepartmentId_BranchDepartmentBranchId",
                table: "Request",
                columns: new[] { "BranchDepartmentDepartmentId", "BranchDepartmentBranchId" });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BranchDepartmentDepartmentId_BranchDepartmentBranchId",
                table: "Employees",
                columns: new[] { "BranchDepartmentDepartmentId", "BranchDepartmentBranchId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_BranchDepartments_BranchDepartmentDepartmentId_BranchDepartmentBranchId",
                table: "Employees",
                columns: new[] { "BranchDepartmentDepartmentId", "BranchDepartmentBranchId" },
                principalTable: "BranchDepartments",
                principalColumns: new[] { "DepartmentId", "BranchId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_BranchDepartments_BranchDepartmentDepartmentId_BranchDepartmentBranchId",
                table: "Request",
                columns: new[] { "BranchDepartmentDepartmentId", "BranchDepartmentBranchId" },
                principalTable: "BranchDepartments",
                principalColumns: new[] { "DepartmentId", "BranchId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
