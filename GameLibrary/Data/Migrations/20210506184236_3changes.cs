using Microsoft.EntityFrameworkCore.Migrations;

namespace GameLibrary.Migrations
{
    public partial class _3changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "GameSystems",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameSystems_userId",
                table: "GameSystems",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameSystems_AspNetUsers_userId",
                table: "GameSystems",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameSystems_AspNetUsers_userId",
                table: "GameSystems");

            migrationBuilder.DropIndex(
                name: "IX_GameSystems_userId",
                table: "GameSystems");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "GameSystems");
        }
    }
}
