using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyMechBackend.Migrations
{
    public partial class correctedColumnBetriebsdauer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Bertriebsdauer",
                schema: "public",
                table: "Maschine",
                newName: "Betriebsdauer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Betriebsdauer",
                schema: "public",
                table: "Maschine",
                newName: "Bertriebsdauer");
        }
    }
}
