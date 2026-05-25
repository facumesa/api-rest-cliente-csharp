using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class Auditoria2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrestamoId",
                table: "Auditorias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Auditorias_PrestamoId",
                table: "Auditorias",
                column: "PrestamoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auditorias_Prestamos_PrestamoId",
                table: "Auditorias",
                column: "PrestamoId",
                principalTable: "Prestamos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auditorias_Prestamos_PrestamoId",
                table: "Auditorias");

            migrationBuilder.DropIndex(
                name: "IX_Auditorias_PrestamoId",
                table: "Auditorias");

            migrationBuilder.DropColumn(
                name: "PrestamoId",
                table: "Auditorias");
        }
    }
}
