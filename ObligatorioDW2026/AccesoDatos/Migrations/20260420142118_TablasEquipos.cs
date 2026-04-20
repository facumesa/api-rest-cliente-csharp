using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class TablasEquipos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Equipos");

            migrationBuilder.DropColumn(
                name: "Resolucion",
                table: "Equipos");

            migrationBuilder.DropColumn(
                name: "TamanioPixel",
                table: "Equipos");

            migrationBuilder.DropColumn(
                name: "TipoSensor",
                table: "Equipos");

            migrationBuilder.CreateTable(
                name: "Camaras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    TipoSensor = table.Column<int>(type: "int", nullable: false),
                    Resolucion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TamanioPixel = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Camaras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Camaras_Equipos_Id",
                        column: x => x.Id,
                        principalTable: "Equipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Monturas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Monturas_Equipos_Id",
                        column: x => x.Id,
                        principalTable: "Equipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Oculares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oculares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Oculares_Equipos_Id",
                        column: x => x.Id,
                        principalTable: "Equipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Telescopios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telescopios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Telescopios_Equipos_Id",
                        column: x => x.Id,
                        principalTable: "Equipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Camaras");

            migrationBuilder.DropTable(
                name: "Monturas");

            migrationBuilder.DropTable(
                name: "Oculares");

            migrationBuilder.DropTable(
                name: "Telescopios");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Equipos",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Resolucion",
                table: "Equipos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TamanioPixel",
                table: "Equipos",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoSensor",
                table: "Equipos",
                type: "int",
                nullable: true);
        }
    }
}
