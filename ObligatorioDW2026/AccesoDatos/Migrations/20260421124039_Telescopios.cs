using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class Telescopios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Apertura_mm",
                table: "Telescopios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DistanciaFocal_mm",
                table: "Telescopios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Peso",
                table: "Telescopios",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "RelacionFocal",
                table: "Telescopios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apertura_mm",
                table: "Telescopios");

            migrationBuilder.DropColumn(
                name: "DistanciaFocal_mm",
                table: "Telescopios");

            migrationBuilder.DropColumn(
                name: "Peso",
                table: "Telescopios");

            migrationBuilder.DropColumn(
                name: "RelacionFocal",
                table: "Telescopios");
        }
    }
}
