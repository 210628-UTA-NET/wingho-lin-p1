using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreAppDL.Migrations
{
    public partial class DatePropAddedToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_StoreFronts_StoreFrontID",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "StoreFrontID",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DatePlaced",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Products_StoreFronts_StoreFrontID",
                table: "Products",
                column: "StoreFrontID",
                principalTable: "StoreFronts",
                principalColumn: "StoreFrontID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_StoreFronts_StoreFrontID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DatePlaced",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "StoreFrontID",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_StoreFronts_StoreFrontID",
                table: "Products",
                column: "StoreFrontID",
                principalTable: "StoreFronts",
                principalColumn: "StoreFrontID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
