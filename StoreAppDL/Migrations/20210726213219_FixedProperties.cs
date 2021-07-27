using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreAppDL.Migrations
{
    public partial class FixedProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_StoreFronts_OrderLocationStoreFrontID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "StoreFronts",
                newName: "StoreFrontName");

            migrationBuilder.RenameColumn(
                name: "OrderLocationStoreFrontID",
                table: "Orders",
                newName: "StoreFrontID");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_OrderLocationStoreFrontID",
                table: "Orders",
                newName: "IX_Orders_StoreFrontID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_StoreFronts_StoreFrontID",
                table: "Orders",
                column: "StoreFrontID",
                principalTable: "StoreFronts",
                principalColumn: "StoreFrontID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_StoreFronts_StoreFrontID",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "StoreFrontName",
                table: "StoreFronts",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "StoreFrontID",
                table: "Orders",
                newName: "OrderLocationStoreFrontID");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_StoreFrontID",
                table: "Orders",
                newName: "IX_Orders_OrderLocationStoreFrontID");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_StoreFronts_OrderLocationStoreFrontID",
                table: "Orders",
                column: "OrderLocationStoreFrontID",
                principalTable: "StoreFronts",
                principalColumn: "StoreFrontID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
