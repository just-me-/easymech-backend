using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyMechBackend.Migrations
{
    public partial class MaschineMitNotizUndJahrgang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maschine_Fahrzeugtyp_FahrzeugtypId",
                schema: "public",
                table: "Maschine");

            migrationBuilder.DropIndex(
                name: "IX_Maschine_FahrzeugtypId",
                schema: "public",
                table: "Maschine");

            migrationBuilder.DropColumn(
                name: "FahrzeugtypId",
                schema: "public",
                table: "Maschine");

            migrationBuilder.DropColumn(
                name: "Jahrgang",
                schema: "public",
                table: "Fahrzeugtyp");

            migrationBuilder.AddColumn<int>(
                name: "Jahrgang",
                schema: "public",
                table: "Maschine",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Notiz",
                schema: "public",
                table: "Maschine",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Jahrgang",
                schema: "public",
                table: "Maschine");

            migrationBuilder.DropColumn(
                name: "Notiz",
                schema: "public",
                table: "Maschine");

            migrationBuilder.AddColumn<long>(
                name: "FahrzeugtypId",
                schema: "public",
                table: "Maschine",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Jahrgang",
                schema: "public",
                table: "Fahrzeugtyp",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Maschine_FahrzeugtypId",
                schema: "public",
                table: "Maschine",
                column: "FahrzeugtypId");

            migrationBuilder.AddForeignKey(
                name: "FK_Maschine_Fahrzeugtyp_FahrzeugtypId",
                schema: "public",
                table: "Maschine",
                column: "FahrzeugtypId",
                principalSchema: "public",
                principalTable: "Fahrzeugtyp",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
