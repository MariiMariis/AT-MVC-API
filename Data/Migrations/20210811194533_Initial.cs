using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fabricantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeFabricante = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fundador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaisOrigem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataFundacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fabricantes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fabricantes");
        }
    }
}
