using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Monthly_Bill_Calculator.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUtilityModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unit",
                table: "Steams");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "NaturalGases");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "HotWaters");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "Electricities");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "ColdWaters");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "CentralHeatings");

            migrationBuilder.InsertData(
                table: "CentralHeatings",
                columns: new[] { "Id", "Consumption", "Price" },
                values: new object[] { 1, 100.0, 0.15m });

            migrationBuilder.InsertData(
                table: "ColdWaters",
                columns: new[] { "Id", "Consumption", "Price" },
                values: new object[] { 1, 5.0, 2.50m });

            migrationBuilder.InsertData(
                table: "Electricities",
                columns: new[] { "Id", "Consumption", "Price" },
                values: new object[] { 1, 120.0, 0.20m });

            migrationBuilder.InsertData(
                table: "HotWaters",
                columns: new[] { "Id", "Consumption", "Price" },
                values: new object[] { 1, 3.0, 4.00m });

            migrationBuilder.InsertData(
                table: "Months",
                columns: new[] { "Id", "CentralHeatingId", "ColdWaterId", "ElectricityId", "HotWaterId", "MonthNumber", "NaturalGasId", "SteamId", "Year" },
                values: new object[,]
                {
                    { 2, null, null, null, null, 12, null, null, 2025 },
                    { 3, null, null, null, null, 1, null, null, 2026 }
                });

            migrationBuilder.InsertData(
                table: "NaturalGases",
                columns: new[] { "Id", "Consumption", "Price" },
                values: new object[] { 1, 50.0, 1.20m });

            migrationBuilder.InsertData(
                table: "Steams",
                columns: new[] { "Id", "Consumption", "Price" },
                values: new object[] { 1, 10.0, 3.00m });

            migrationBuilder.InsertData(
                table: "Months",
                columns: new[] { "Id", "CentralHeatingId", "ColdWaterId", "ElectricityId", "HotWaterId", "MonthNumber", "NaturalGasId", "SteamId", "Year" },
                values: new object[] { 1, 1, 1, 1, 1, 11, 1, 1, 2025 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Months",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Months",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Months",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CentralHeatings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ColdWaters",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Electricities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "HotWaters",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "NaturalGases",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Steams",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "Steams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "NaturalGases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "HotWaters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "Electricities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "ColdWaters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "CentralHeatings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
