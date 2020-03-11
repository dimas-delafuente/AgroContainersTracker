using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AgroContainerTracker.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Containers",
                columns: table => new
                {
                    ContainerId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Size = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Containers", x => x.ContainerId);
                });

            migrationBuilder.CreateTable(
                name: "FruitBuyers",
                columns: table => new
                {
                    FruitBuyerId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FruitBuyers", x => x.FruitBuyerId);
                });

            migrationBuilder.CreateTable(
                name: "FruitGrowers",
                columns: table => new
                {
                    FruitBuyerId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FruitGrowers", x => x.FruitBuyerId);
                });

            migrationBuilder.CreateTable(
                name: "Fruits",
                columns: table => new
                {
                    FruitId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fruits", x => x.FruitId);
                });

            migrationBuilder.CreateTable(
                name: "Palots",
                columns: table => new
                {
                    PalotId = table.Column<string>(nullable: false),
                    ArrivalNumber = table.Column<string>(nullable: false),
                    Arrival = table.Column<DateTime>(nullable: false),
                    Departure = table.Column<DateTime>(nullable: false),
                    Weight = table.Column<double>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    FruitGrowerId = table.Column<Guid>(nullable: false),
                    FruitBuyerId = table.Column<Guid>(nullable: false),
                    FruitId = table.Column<Guid>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    ContainerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Palots", x => x.PalotId);
                    table.ForeignKey(
                        name: "FK_Palots_Containers_ContainerId",
                        column: x => x.ContainerId,
                        principalTable: "Containers",
                        principalColumn: "ContainerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Palots_FruitBuyers_FruitBuyerId",
                        column: x => x.FruitBuyerId,
                        principalTable: "FruitBuyers",
                        principalColumn: "FruitBuyerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Palots_FruitGrowers_FruitGrowerId",
                        column: x => x.FruitGrowerId,
                        principalTable: "FruitGrowers",
                        principalColumn: "FruitBuyerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Palots_Fruits_FruitId",
                        column: x => x.FruitId,
                        principalTable: "Fruits",
                        principalColumn: "FruitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Palots_ContainerId",
                table: "Palots",
                column: "ContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_Palots_FruitBuyerId",
                table: "Palots",
                column: "FruitBuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Palots_FruitGrowerId",
                table: "Palots",
                column: "FruitGrowerId");

            migrationBuilder.CreateIndex(
                name: "IX_Palots_FruitId",
                table: "Palots",
                column: "FruitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Palots");

            migrationBuilder.DropTable(
                name: "Containers");

            migrationBuilder.DropTable(
                name: "FruitBuyers");

            migrationBuilder.DropTable(
                name: "FruitGrowers");

            migrationBuilder.DropTable(
                name: "Fruits");
        }
    }
}
