using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinimalAPI_Pagos.Migrations
{
    public partial class ADD_TABLE_PRECIO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Precio",
                columns: table => new
                {
                    idPrecio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Monto = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Precio", x => x.idPrecio);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Precio");
        }
    }
}
