using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WS02._1.Migrations
{
    public partial class MigracaoInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Paciente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome_paciente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nome_doutor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tipo_quarto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    quarto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paciente", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Paciente");
        }
    }
}
