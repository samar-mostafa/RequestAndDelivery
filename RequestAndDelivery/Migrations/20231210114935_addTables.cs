using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RequestAndDelivery.Migrations
{
    public partial class addTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeliverd",
                table: "Request",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    SerialNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsNew = table.Column<bool>(type: "bit", nullable: false),
                    EmployeeDeliverToId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeDeliverFromId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.SerialNumber);
                    table.ForeignKey(
                        name: "FK_Devices_Employees_EmployeeDeliverFromId",
                        column: x => x.EmployeeDeliverFromId,
                        principalTable: "Employees",
                        principalColumn: "MobileNumber");
                    table.ForeignKey(
                        name: "FK_Devices_Employees_EmployeeDeliverToId",
                        column: x => x.EmployeeDeliverToId,
                        principalTable: "Employees",
                        principalColumn: "MobileNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Delivaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DelivaryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    DeviceSerialNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RequestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delivaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Delivaries_Devices_DeviceSerialNumber",
                        column: x => x.DeviceSerialNumber,
                        principalTable: "Devices",
                        principalColumn: "SerialNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Delivaries_Request_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Delivaries_DeviceSerialNumber",
                table: "Delivaries",
                column: "DeviceSerialNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Delivaries_RequestId",
                table: "Delivaries",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_EmployeeDeliverFromId",
                table: "Devices",
                column: "EmployeeDeliverFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_EmployeeDeliverToId",
                table: "Devices",
                column: "EmployeeDeliverToId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Delivaries");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropColumn(
                name: "IsDeliverd",
                table: "Request");
        }
    }
}
