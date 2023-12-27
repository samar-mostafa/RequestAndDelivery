using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RequestAndDelivery.Migrations
{
    public partial class editTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Delivaries_Devices_DeviceSerialNumber",
                table: "Delivaries");

            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Employees_EmployeeDeliverToId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Employees_EmployeeId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Delivaries_DeviceSerialNumber",
                table: "Delivaries");

            migrationBuilder.DropColumn(
                name: "DeviceSerialNumber",
                table: "Delivaries");

            migrationBuilder.AlterColumn<string>(
                name: "ExportNumber",
                table: "Requests",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "Requests",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "DeviceTypes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "Devices",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeDeliverToId",
                table: "Devices",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DeviceId",
                table: "Delivaries",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Branchs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ExportNumber",
                table: "Requests",
                column: "ExportNumber",
                unique: true,
                filter: "[ExportNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Delivaries_DeviceId",
                table: "Delivaries",
                column: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Delivaries_Devices_DeviceId",
                table: "Delivaries",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "SerialNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Employees_EmployeeDeliverToId",
                table: "Devices",
                column: "EmployeeDeliverToId",
                principalTable: "Employees",
                principalColumn: "MobileNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Employees_EmployeeId",
                table: "Requests",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "MobileNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Delivaries_Devices_DeviceId",
                table: "Delivaries");

            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Employees_EmployeeDeliverToId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Employees_EmployeeId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_ExportNumber",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Delivaries_DeviceId",
                table: "Delivaries");

            migrationBuilder.AlterColumn<string>(
                name: "ExportNumber",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "Requests",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "DeviceTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "Devices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeDeliverToId",
                table: "Devices",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DeviceId",
                table: "Delivaries",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeviceSerialNumber",
                table: "Delivaries",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Branchs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Delivaries_DeviceSerialNumber",
                table: "Delivaries",
                column: "DeviceSerialNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Delivaries_Devices_DeviceSerialNumber",
                table: "Delivaries",
                column: "DeviceSerialNumber",
                principalTable: "Devices",
                principalColumn: "SerialNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Employees_EmployeeDeliverToId",
                table: "Devices",
                column: "EmployeeDeliverToId",
                principalTable: "Employees",
                principalColumn: "MobileNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Employees_EmployeeId",
                table: "Requests",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "MobileNumber",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
