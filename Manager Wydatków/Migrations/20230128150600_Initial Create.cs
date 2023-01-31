using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagerWydatkow.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategorias",
                columns: table => new
                {
                    KategoriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tytul = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Ikona = table.Column<string>(type: "nvarchar(5)", nullable: false),
                    Typ = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorias", x => x.KategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "Transakcjes",
                columns: table => new
                {
                    TransakcjeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KategoriaId = table.Column<int>(type: "int", nullable: false),
                    Cena = table.Column<int>(type: "int", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(75)", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transakcjes", x => x.TransakcjeId);
                    table.ForeignKey(
                        name: "FK_Transakcjes_Kategorias_KategoriaId",
                        column: x => x.KategoriaId,
                        principalTable: "Kategorias",
                        principalColumn: "KategoriaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transakcjes_KategoriaId",
                table: "Transakcjes",
                column: "KategoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transakcjes");

            migrationBuilder.DropTable(
                name: "Kategorias");
        }
    }
}
