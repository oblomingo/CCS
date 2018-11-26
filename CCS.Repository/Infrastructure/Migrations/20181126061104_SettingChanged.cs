using Microsoft.EntityFrameworkCore.Migrations;

namespace CCS.Repository.Infrastructure.Migrations
{
    public partial class SettingChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Settings",
                schema: "station",
                newName: "Settings");

            migrationBuilder.RenameTable(
                name: "Measures",
                schema: "station",
                newName: "Measures");

            migrationBuilder.AddColumn<bool>(
                name: "IsOn",
                table: "Settings",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOn",
                table: "Settings");

            migrationBuilder.EnsureSchema(
                name: "station");

            migrationBuilder.RenameTable(
                name: "Settings",
                newName: "Settings",
                newSchema: "station");

            migrationBuilder.RenameTable(
                name: "Measures",
                newName: "Measures",
                newSchema: "station");
        }
    }
}
