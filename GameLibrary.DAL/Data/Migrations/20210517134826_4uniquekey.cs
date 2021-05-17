using Microsoft.EntityFrameworkCore.Migrations;

namespace GameLibrary.Migrations
{
    public partial class _4uniquekey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GameShopName",
                table: "GameShops",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_GameSystems_SystemName",
                table: "GameSystems",
                column: "SystemName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameShops_GameShopName",
                table: "GameShops",
                column: "GameShopName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameLibraries_Name",
                table: "GameLibraries",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GameSystems_SystemName",
                table: "GameSystems");

            migrationBuilder.DropIndex(
                name: "IX_GameShops_GameShopName",
                table: "GameShops");

            migrationBuilder.DropIndex(
                name: "IX_GameLibraries_Name",
                table: "GameLibraries");

            migrationBuilder.AlterColumn<string>(
                name: "GameShopName",
                table: "GameShops",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
