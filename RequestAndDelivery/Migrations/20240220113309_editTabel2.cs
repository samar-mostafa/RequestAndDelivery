using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RequestAndDelivery.Migrations
{
    /// <inheritdoc />
    public partial class editTabel2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Employees_EmployeeDeliverFromId",
                table: "Devices");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeDeliverFromId",
                table: "Devices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Employees_EmployeeDeliverFromId",
                table: "Devices",
                column: "EmployeeDeliverFromId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Employees_EmployeeDeliverFromId",
                table: "Devices");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeDeliverFromId",
                table: "Devices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Employees_EmployeeDeliverFromId",
                table: "Devices",
                column: "EmployeeDeliverFromId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
