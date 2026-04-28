using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class Prestamos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prestamos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    SocioId = table.Column<int>(type: "int", nullable: false),
                    CoordinadorId = table.Column<int>(type: "int", nullable: false),
                    MonturaId = table.Column<int>(type: "int", nullable: false),
                    TelescopioId = table.Column<int>(type: "int", nullable: false),
                    CamaraId = table.Column<int>(type: "int", nullable: true),
                    OcularId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prestamos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prestamos_Camaras_CamaraId",
                        column: x => x.CamaraId,
                        principalTable: "Camaras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prestamos_Monturas_MonturaId",
                        column: x => x.MonturaId,
                        principalTable: "Monturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prestamos_Oculares_OcularId",
                        column: x => x.OcularId,
                        principalTable: "Oculares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prestamos_Telescopios_TelescopioId",
                        column: x => x.TelescopioId,
                        principalTable: "Telescopios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prestamos_Usuarios_CoordinadorId",
                        column: x => x.CoordinadorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prestamos_Usuarios_SocioId",
                        column: x => x.SocioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_CamaraId",
                table: "Prestamos",
                column: "CamaraId");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_CoordinadorId",
                table: "Prestamos",
                column: "CoordinadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_MonturaId",
                table: "Prestamos",
                column: "MonturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_OcularId",
                table: "Prestamos",
                column: "OcularId");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_SocioId",
                table: "Prestamos",
                column: "SocioId");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_TelescopioId",
                table: "Prestamos",
                column: "TelescopioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prestamos");
        }
    }
}
