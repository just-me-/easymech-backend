using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EasyMechBackend.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "Kunden",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Firma = table.Column<string>(maxLength: 128, nullable: false),
                    Vorname = table.Column<string>(maxLength: 128, nullable: true),
                    Nachname = table.Column<string>(maxLength: 128, nullable: true),
                    Adresse = table.Column<string>(maxLength: 128, nullable: true),
                    PLZ = table.Column<string>(maxLength: 128, nullable: true),
                    Ort = table.Column<string>(maxLength: 128, nullable: true),
                    Email = table.Column<string>(maxLength: 128, nullable: true),
                    Telefon = table.Column<string>(maxLength: 128, nullable: true),
                    Notiz = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kunden", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kunden",
                schema: "public");
        }
    }
}
