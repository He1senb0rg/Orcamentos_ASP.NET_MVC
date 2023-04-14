using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orcamentos.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "buManagers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_buManagers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "profileLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profileLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "revenueTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_revenueTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "businessUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    buManagerId = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_businessUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_businessUnits_buManagers_buManagerId",
                        column: x => x.buManagerId,
                        principalTable: "buManagers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "profiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    profileLevelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_profiles_profileLevels_profileLevelId",
                        column: x => x.profileLevelId,
                        principalTable: "profileLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orcamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    profileId = table.Column<int>(type: "int", nullable: false),
                    revenueTypeId = table.Column<int>(type: "int", nullable: false),
                    businessUnitId = table.Column<int>(type: "int", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoUni = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Partnumb = table.Column<int>(type: "int", nullable: false),
                    modelo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SerialNumb = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orcamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orcamentos_businessUnits_businessUnitId",
                        column: x => x.businessUnitId,
                        principalTable: "businessUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orcamentos_profiles_profileId",
                        column: x => x.profileId,
                        principalTable: "profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orcamentos_revenueTypes_revenueTypeId",
                        column: x => x.revenueTypeId,
                        principalTable: "revenueTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_businessUnits_buManagerId",
                table: "businessUnits",
                column: "buManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_orcamentos_businessUnitId",
                table: "orcamentos",
                column: "businessUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_orcamentos_profileId",
                table: "orcamentos",
                column: "profileId");

            migrationBuilder.CreateIndex(
                name: "IX_orcamentos_revenueTypeId",
                table: "orcamentos",
                column: "revenueTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_profiles_profileLevelId",
                table: "profiles",
                column: "profileLevelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orcamentos");

            migrationBuilder.DropTable(
                name: "businessUnits");

            migrationBuilder.DropTable(
                name: "profiles");

            migrationBuilder.DropTable(
                name: "revenueTypes");

            migrationBuilder.DropTable(
                name: "buManagers");

            migrationBuilder.DropTable(
                name: "profileLevels");
        }
    }
}
