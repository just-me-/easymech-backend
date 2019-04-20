using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EasyMechBackend.Migrations
{
    public partial class MitMaschineUndTyp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fahrzeugtyp",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Fabrikat = table.Column<string>(maxLength: 128, nullable: true),
                    Motortyp = table.Column<string>(maxLength: 128, nullable: true),
                    Nutzlast = table.Column<int>(nullable: false),
                    Hubkraft = table.Column<int>(nullable: false),
                    Hubhöhe = table.Column<int>(nullable: false),
                    Eigengewicht = table.Column<int>(nullable: false),
                    Jahrgang = table.Column<int>(nullable: false),
                    Fahrzeughöhe = table.Column<int>(nullable: false),
                    Fahrzeuglänge = table.Column<int>(nullable: false),
                    Fahrzeugbreite = table.Column<int>(nullable: false),
                    Pneugrösse = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fahrzeugtyp", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Maschine",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Seriennummer = table.Column<string>(maxLength: 128, nullable: true),
                    Mastnummer = table.Column<string>(maxLength: 128, nullable: true),
                    Motorennummer = table.Column<string>(maxLength: 128, nullable: true),
                    Bertriebsdauer = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    TypId = table.Column<long>(nullable: false),
                    BesitzerId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maschine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Maschine_Kunden_BesitzerId",
                        column: x => x.BesitzerId,
                        principalSchema: "public",
                        principalTable: "Kunden",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Maschine_Fahrzeugtyp_TypId",
                        column: x => x.TypId,
                        principalSchema: "public",
                        principalTable: "Fahrzeugtyp",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Maschine_BesitzerId",
                schema: "public",
                table: "Maschine",
                column: "BesitzerId");

            migrationBuilder.CreateIndex(
                name: "IX_Maschine_TypId",
                schema: "public",
                table: "Maschine",
                column: "TypId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Maschine",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Fahrzeugtyp",
                schema: "public");
        }
    }
}
