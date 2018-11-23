using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CCS.Repository.Infrastructure.Migrations
{
    public partial class MeasuresAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "station");

            migrationBuilder.CreateTable(
                name: "Measures",
                schema: "station",
                columns: table => new
                {
                    MeasureId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Location = table.Column<int>(nullable: false),
                    Temperature = table.Column<decimal>(nullable: false),
                    Humidity = table.Column<decimal>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measures", x => x.MeasureId);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                schema: "station",
                columns: table => new
                {
                    SettingId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InnerTemperatureMin = table.Column<int>(nullable: false),
                    InnerTemperatureMax = table.Column<int>(nullable: false),
                    OuterTemperatureMin = table.Column<int>(nullable: false),
                    OuterTemperatureMax = table.Column<int>(nullable: false),
                    Mode = table.Column<int>(nullable: false),
                    ScheduleStar = table.Column<DateTime>(nullable: false),
                    ScheduleStop = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.SettingId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Measures",
                schema: "station");

            migrationBuilder.DropTable(
                name: "Settings",
                schema: "station");
        }
    }
}
