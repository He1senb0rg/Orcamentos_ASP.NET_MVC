using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orcamentos.Migrations
{
    public partial class teste : Migration
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
                name: "orcamentoNomes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProposalNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orcamentoNomes", x => x.Id);
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
                    orcamentoNomeId = table.Column<int>(type: "int", nullable: false),
                    profileId = table.Column<int>(type: "int", nullable: false),
                    revenueTypeId = table.Column<int>(type: "int", nullable: false),
                    businessUnitId = table.Column<int>(type: "int", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoUni = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Partnumb = table.Column<int>(type: "int", nullable: false),
                    modelo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SerialNumb = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DescontoTabela = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecoParcial = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustoTabela = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustoDesc1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustoDesc2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustoDesc3 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Margin = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MG = table.Column<decimal>(type: "decimal(14,3)", precision: 14, scale: 3, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DelivaryDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExternalProvider = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                        name: "FK_orcamentos_orcamentoNomes_orcamentoNomeId",
                        column: x => x.orcamentoNomeId,
                        principalTable: "orcamentoNomes",
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
                name: "IX_orcamentos_orcamentoNomeId",
                table: "orcamentos",
                column: "orcamentoNomeId");

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
                name: "orcamentoNomes");

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
