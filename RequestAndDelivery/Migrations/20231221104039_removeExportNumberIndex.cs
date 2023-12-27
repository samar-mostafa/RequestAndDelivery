using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RequestAndDelivery.Migrations
{
    public partial class removeExportNumberIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Requests_ExportNumber",
                table: "Requests");

            migrationBuilder.AlterColumn<string>(
                name: "ExportNumber",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ExportNumber",
                table: "Requests",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ExportNumber",
                table: "Requests",
                column: "ExportNumber",
                unique: true,
                filter: "[ExportNumber] IS NOT NULL");
        }
    }
}
