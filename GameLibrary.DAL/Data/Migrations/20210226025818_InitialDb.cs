using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameLibrary.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameSystems",
                columns: table => new
                {
                    GameSystemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSystems", x => x.GameSystemID);
                });

            migrationBuilder.CreateTable(
                name: "GameLibraries",
                columns: table => new
                {
                    GameLibraryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameSystemID = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    DiscType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameLibraries", x => x.GameLibraryID);
                    table.ForeignKey(
                        name: "FK_GameLibraries_GameSystems_GameSystemID",
                        column: x => x.GameSystemID,
                        principalTable: "GameSystems",
                        principalColumn: "GameSystemID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "GameShops",
                columns: table => new
                {
                    GameShopID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameShopName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameLibraryID = table.Column<int>(type: "int", nullable: false),
                    GameSystemID = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameShops", x => x.GameShopID);
                    table.ForeignKey(
                        name: "FK_GameShops_GameLibraries_GameLibraryID",
                        column: x => x.GameLibraryID,
                        principalTable: "GameLibraries",
                        principalColumn: "GameLibraryID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_GameShops_GameSystems_GameSystemID",
                        column: x => x.GameSystemID,
                        principalTable: "GameSystems",
                        principalColumn: "GameSystemID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameLibraries_GameSystemID",
                table: "GameLibraries",
                column: "GameSystemID");

            migrationBuilder.CreateIndex(
                name: "IX_GameShops_GameLibraryID",
                table: "GameShops",
                column: "GameLibraryID");

            migrationBuilder.CreateIndex(
                name: "IX_GameShops_GameSystemID",
                table: "GameShops",
                column: "GameSystemID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameShops");

            migrationBuilder.DropTable(
                name: "GameLibraries");

            migrationBuilder.DropTable(
                name: "GameSystems");
        }
    }
}
