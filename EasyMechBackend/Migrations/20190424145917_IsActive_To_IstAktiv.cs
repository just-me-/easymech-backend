using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyMechBackend.Migrations
{
    public partial class IsActive_To_IstAktiv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                schema: "public",
                table: "Maschine",
                newName: "IstAktiv");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                schema: "public",
                table: "Kunden",
                newName: "IstAktiv");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IstAktiv",
                schema: "public",
                table: "Maschine",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IstAktiv",
                schema: "public",
                table: "Kunden",
                newName: "IsActive");
        }
    }
}
