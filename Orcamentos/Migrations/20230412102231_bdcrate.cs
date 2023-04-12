using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orcamentos.Migrations
{
    public partial class bdcrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "buManagers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_buManagers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "profileLevels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profileLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "revenueTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_revenueTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "businessUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BumId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_businessUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_businessUnits_buManagers_BuManagerId",
                        column: x => x.BuManagerId,
                        principalTable: "buManagers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "profiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    ProfileLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_profiles_profileLevels_ProfileLevelId",
                        column: x => x.ProfileLevelId,
                        principalTable: "profileLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orcamentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RevTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RevenueTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoUni = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Partnumb = table.Column<int>(type: "int", nullable: false),
                    modelo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SerialNumb = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orcamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orcamentos_buManagers_BuManagerId",
                        column: x => x.BuManagerId,
                        principalTable: "buManagers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orcamentos_profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orcamentos_revenueTypes_RevenueTypeId",
                        column: x => x.RevenueTypeId,
                        principalTable: "revenueTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_businessUnits_BuManagerId",
                table: "businessUnits",
                column: "BuManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_orcamentos_BuManagerId",
                table: "orcamentos",
                column: "BuManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_orcamentos_ProfileId",
                table: "orcamentos",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_orcamentos_RevenueTypeId",
                table: "orcamentos",
                column: "RevenueTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_profiles_ProfileLevelId",
                table: "profiles",
                column: "ProfileLevelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "businessUnits");

            migrationBuilder.DropTable(
                name: "orcamentos");

            migrationBuilder.DropTable(
                name: "buManagers");

            migrationBuilder.DropTable(
                name: "profiles");

            migrationBuilder.DropTable(
                name: "revenueTypes");

            migrationBuilder.DropTable(
                name: "profileLevels");
        }
    }
}
