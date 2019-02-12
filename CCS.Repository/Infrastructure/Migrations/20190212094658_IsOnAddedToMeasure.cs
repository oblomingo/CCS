using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CCS.Repository.Infrastructure.Migrations
{
    public partial class IsOnAddedToMeasure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOn",
                table: "Measures",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "SettingId", "InnerTemperatureMax", "InnerTemperatureMin", "IsOn", "Mode", "OuterTemperatureMax", "OuterTemperatureMin", "ScheduleStar", "ScheduleStop" },
                values: new object[] { 1, 0, 3, false, 1, 0, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "SettingId",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "IsOn",
                table: "Measures");
        }
    }
}
