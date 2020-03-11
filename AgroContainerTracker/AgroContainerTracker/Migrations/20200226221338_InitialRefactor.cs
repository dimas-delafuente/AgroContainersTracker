using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AgroContainerTracker.Migrations
{
    public partial class InitialRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Palots_Containers_ContainerId",
                table: "Palots");

            migrationBuilder.DropForeignKey(
                name: "FK_Palots_FruitBuyers_FruitBuyerId",
                table: "Palots");

            migrationBuilder.DropForeignKey(
                name: "FK_Palots_FruitGrowers_FruitGrowerId",
                table: "Palots");

            migrationBuilder.DropTable(
                name: "FruitBuyers");

            migrationBuilder.DropTable(
                name: "FruitGrowers");

            migrationBuilder.DropIndex(
                name: "IX_Palots_ContainerId",
                table: "Palots");

            migrationBuilder.DropColumn(
                name: "ContainerId",
                table: "Palots");

            migrationBuilder.AddColumn<int>(
                name: "ContainerEntityContainerId",
                table: "Palots",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FruitVendors",
                columns: table => new
                {
                    FruitVendorId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FruitVendors", x => x.FruitVendorId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Palots_ContainerEntityContainerId",
                table: "Palots",
                column: "ContainerEntityContainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Palots_Containers_ContainerEntityContainerId",
                table: "Palots",
                column: "ContainerEntityContainerId",
                principalTable: "Containers",
                principalColumn: "ContainerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Palots_FruitVendors_FruitBuyerId",
                table: "Palots",
                column: "FruitBuyerId",
                principalTable: "FruitVendors",
                principalColumn: "FruitVendorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Palots_FruitVendors_FruitGrowerId",
                table: "Palots",
                column: "FruitGrowerId",
                principalTable: "FruitVendors",
                principalColumn: "FruitVendorId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Palots_Containers_ContainerEntityContainerId",
                table: "Palots");

            migrationBuilder.DropForeignKey(
                name: "FK_Palots_FruitVendors_FruitBuyerId",
                table: "Palots");

            migrationBuilder.DropForeignKey(
                name: "FK_Palots_FruitVendors_FruitGrowerId",
                table: "Palots");

            migrationBuilder.DropTable(
                name: "FruitVendors");

            migrationBuilder.DropIndex(
                name: "IX_Palots_ContainerEntityContainerId",
                table: "Palots");

            migrationBuilder.DropColumn(
                name: "ContainerEntityContainerId",
                table: "Palots");

            migrationBuilder.AddColumn<int>(
                name: "ContainerId",
                table: "Palots",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FruitBuyers",
                columns: table => new
                {
                    FruitBuyerId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FruitBuyers", x => x.FruitBuyerId);
                });

            migrationBuilder.CreateTable(
                name: "FruitGrowers",
                columns: table => new
                {
                    FruitBuyerId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FruitGrowers", x => x.FruitBuyerId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Palots_ContainerId",
                table: "Palots",
                column: "ContainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Palots_Containers_ContainerId",
                table: "Palots",
                column: "ContainerId",
                principalTable: "Containers",
                principalColumn: "ContainerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Palots_FruitBuyers_FruitBuyerId",
                table: "Palots",
                column: "FruitBuyerId",
                principalTable: "FruitBuyers",
                principalColumn: "FruitBuyerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Palots_FruitGrowers_FruitGrowerId",
                table: "Palots",
                column: "FruitGrowerId",
                principalTable: "FruitGrowers",
                principalColumn: "FruitBuyerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
