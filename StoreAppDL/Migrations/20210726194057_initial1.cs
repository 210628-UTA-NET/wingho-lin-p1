using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreAppDL.Migrations
{
    public partial class initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LineItems_Customers_CustomerID",
                table: "LineItems");

            migrationBuilder.DropIndex(
                name: "IX_LineItems_CustomerID",
                table: "LineItems");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "LineItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerID",
                table: "LineItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_CustomerID",
                table: "LineItems",
                column: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_LineItems_Customers_CustomerID",
                table: "LineItems",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
