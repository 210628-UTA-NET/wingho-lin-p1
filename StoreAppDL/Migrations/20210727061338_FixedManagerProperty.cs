using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreAppDL.Migrations
{
    public partial class FixedManagerProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_StoreFronts_ManagerStoreStoreFrontID",
                table: "Managers");

            migrationBuilder.RenameColumn(
                name: "ManagerStoreStoreFrontID",
                table: "Managers",
                newName: "StoreFrontID");

            migrationBuilder.RenameIndex(
                name: "IX_Managers_ManagerStoreStoreFrontID",
                table: "Managers",
                newName: "IX_Managers_StoreFrontID");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_StoreFronts_StoreFrontID",
                table: "Managers",
                column: "StoreFrontID",
                principalTable: "StoreFronts",
                principalColumn: "StoreFrontID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_StoreFronts_StoreFrontID",
                table: "Managers");

            migrationBuilder.RenameColumn(
                name: "StoreFrontID",
                table: "Managers",
                newName: "ManagerStoreStoreFrontID");

            migrationBuilder.RenameIndex(
                name: "IX_Managers_StoreFrontID",
                table: "Managers",
                newName: "IX_Managers_ManagerStoreStoreFrontID");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_StoreFronts_ManagerStoreStoreFrontID",
                table: "Managers",
                column: "ManagerStoreStoreFrontID",
                principalTable: "StoreFronts",
                principalColumn: "StoreFrontID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
