using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyMechBackend.Migrations
{
    public partial class removedUmlaute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Pneugrösse",
                schema: "public",
                table: "Fahrzeugtyp",
                newName: "Pneugroesse");

            migrationBuilder.RenameColumn(
                name: "Hubhöhe",
                schema: "public",
                table: "Fahrzeugtyp",
                newName: "Hubhoehe");

            migrationBuilder.RenameColumn(
                name: "Fahrzeuglänge",
                schema: "public",
                table: "Fahrzeugtyp",
                newName: "Fahrzeuglaenge");

            migrationBuilder.RenameColumn(
                name: "Fahrzeughöhe",
                schema: "public",
                table: "Fahrzeugtyp",
                newName: "Fahrzeughoehe");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Pneugroesse",
                schema: "public",
                table: "Fahrzeugtyp",
                newName: "Pneugrösse");

            migrationBuilder.RenameColumn(
                name: "Hubhoehe",
                schema: "public",
                table: "Fahrzeugtyp",
                newName: "Hubhöhe");

            migrationBuilder.RenameColumn(
                name: "Fahrzeuglaenge",
                schema: "public",
                table: "Fahrzeugtyp",
                newName: "Fahrzeuglänge");

            migrationBuilder.RenameColumn(
                name: "Fahrzeughoehe",
                schema: "public",
                table: "Fahrzeugtyp",
                newName: "Fahrzeughöhe");
        }
    }
}
