using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyMechBackend.Migrations
{
    public partial class maschineOhneTyp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maschine_Fahrzeugtyp_TypId",
                schema: "public",
                table: "Maschine");

            migrationBuilder.DropIndex(
                name: "IX_Maschine_TypId",
                schema: "public",
                table: "Maschine");

            migrationBuilder.DropColumn(
                name: "TypId",
                schema: "public",
                table: "Maschine");

            migrationBuilder.AddColumn<long>(
                name: "FahrzeugtypId",
                schema: "public",
                table: "Maschine",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<long>(
                name: "TypId",
                schema: "public",
                table: "Maschine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Maschine_TypId",
                schema: "public",
                table: "Maschine",
                column: "TypId");

            migrationBuilder.AddForeignKey(
                name: "FK_Maschine_Fahrzeugtyp_TypId",
                schema: "public",
                table: "Maschine",
                column: "TypId",
                principalSchema: "public",
                principalTable: "Fahrzeugtyp",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
