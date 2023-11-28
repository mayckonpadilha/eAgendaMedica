using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eAgendaMedica.Infra.Orm.Migrations
{
    public partial class PostgreMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBAtividade",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Assunto = table.Column<string>(type: "varchar(200)", nullable: false),
                    DataRealizacao = table.Column<DateTime>(type: "date", nullable: false),
                    HoraInicio = table.Column<long>(type: "bigint", nullable: false),
                    HoraTermino = table.Column<long>(type: "bigint", nullable: false),
                    Finalizada = table.Column<bool>(type: "bool", nullable: false),
                    TipoAtividadeEnum = table.Column<int>(type: "integer", nullable: false),
                    TempoDeDescanso = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBAtividade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBMedico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CRM = table.Column<string>(type: "char(8)", nullable: false),
                    Nome = table.Column<string>(type: "varchar(200)", nullable: false),
                    EmAtividade = table.Column<bool>(type: "bool", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBMedico", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBAtividade_TBMedico",
                columns: table => new
                {
                    AtividadesId = table.Column<Guid>(type: "uuid", nullable: false),
                    MedicosId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBAtividade_TBMedico", x => new { x.AtividadesId, x.MedicosId });
                    table.ForeignKey(
                        name: "FK_TBAtividade_TBMedico_TBAtividade_AtividadesId",
                        column: x => x.AtividadesId,
                        principalTable: "TBAtividade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBAtividade_TBMedico_TBMedico_MedicosId",
                        column: x => x.MedicosId,
                        principalTable: "TBMedico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBHoraOcupadas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DiaDaAtividade = table.Column<DateTime>(type: "date", nullable: false),
                    HoraInicio = table.Column<long>(type: "bigint", nullable: false),
                    HoraFinal = table.Column<long>(type: "bigint", nullable: false),
                    MedicoId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBHoraOcupadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBHoraOcupadas_TBMedico_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "TBMedico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBAtividade_TBMedico_MedicosId",
                table: "TBAtividade_TBMedico",
                column: "MedicosId");

            migrationBuilder.CreateIndex(
                name: "IX_TBHoraOcupadas_MedicoId",
                table: "TBHoraOcupadas",
                column: "MedicoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBAtividade_TBMedico");

            migrationBuilder.DropTable(
                name: "TBHoraOcupadas");

            migrationBuilder.DropTable(
                name: "TBAtividade");

            migrationBuilder.DropTable(
                name: "TBMedico");
        }
    }
}
