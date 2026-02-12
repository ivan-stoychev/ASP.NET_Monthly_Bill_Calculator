using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Monthly_Bill_Calculator.Migrations
{
    /// <inheritdoc />
    public partial class InitialDbDesign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CentralHeatings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Consumption = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentralHeatings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ColdWaters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Consumption = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColdWaters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Electricities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Consumption = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Electricities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HotWaters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Consumption = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotWaters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NaturalGases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Consumption = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NaturalGases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Steams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Consumption = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Steams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Months",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: false),
                    MonthNumber = table.Column<int>(type: "int", nullable: false),
                    ElectricityId = table.Column<int>(type: "int", nullable: true),
                    ColdWaterId = table.Column<int>(type: "int", nullable: true),
                    HotWaterId = table.Column<int>(type: "int", nullable: true),
                    NaturalGasId = table.Column<int>(type: "int", nullable: true),
                    SteamId = table.Column<int>(type: "int", nullable: true),
                    CentralHeatingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Months", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Months_CentralHeatings_CentralHeatingId",
                        column: x => x.CentralHeatingId,
                        principalTable: "CentralHeatings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Months_ColdWaters_ColdWaterId",
                        column: x => x.ColdWaterId,
                        principalTable: "ColdWaters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Months_Electricities_ElectricityId",
                        column: x => x.ElectricityId,
                        principalTable: "Electricities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Months_HotWaters_HotWaterId",
                        column: x => x.HotWaterId,
                        principalTable: "HotWaters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Months_NaturalGases_NaturalGasId",
                        column: x => x.NaturalGasId,
                        principalTable: "NaturalGases",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Months_Steams_SteamId",
                        column: x => x.SteamId,
                        principalTable: "Steams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Months_CentralHeatingId",
                table: "Months",
                column: "CentralHeatingId");

            migrationBuilder.CreateIndex(
                name: "IX_Months_ColdWaterId",
                table: "Months",
                column: "ColdWaterId");

            migrationBuilder.CreateIndex(
                name: "IX_Months_ElectricityId",
                table: "Months",
                column: "ElectricityId");

            migrationBuilder.CreateIndex(
                name: "IX_Months_HotWaterId",
                table: "Months",
                column: "HotWaterId");

            migrationBuilder.CreateIndex(
                name: "IX_Months_NaturalGasId",
                table: "Months",
                column: "NaturalGasId");

            migrationBuilder.CreateIndex(
                name: "IX_Months_SteamId",
                table: "Months",
                column: "SteamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Months");

            migrationBuilder.DropTable(
                name: "CentralHeatings");

            migrationBuilder.DropTable(
                name: "ColdWaters");

            migrationBuilder.DropTable(
                name: "Electricities");

            migrationBuilder.DropTable(
                name: "HotWaters");

            migrationBuilder.DropTable(
                name: "NaturalGases");

            migrationBuilder.DropTable(
                name: "Steams");
        }
    }
}
