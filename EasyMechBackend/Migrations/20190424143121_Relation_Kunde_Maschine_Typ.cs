using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyMechBackend.Migrations
{
    public partial class Relation_Kunde_Maschine_Typ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "MaschinenTypId",
                schema: "public",
                table: "Maschine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Maschine_MaschinenTypId",
                schema: "public",
                table: "Maschine",
                column: "MaschinenTypId");

            migrationBuilder.AddForeignKey(
                name: "FK_Maschine_Fahrzeugtyp_MaschinenTypId",
                schema: "public",
                table: "Maschine",
                column: "MaschinenTypId",
                principalSchema: "public",
                principalTable: "Fahrzeugtyp",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maschine_Fahrzeugtyp_MaschinenTypId",
                schema: "public",
                table: "Maschine");

            migrationBuilder.DropIndex(
                name: "IX_Maschine_MaschinenTypId",
                schema: "public",
                table: "Maschine");

            migrationBuilder.DropColumn(
                name: "MaschinenTypId",
                schema: "public",
                table: "Maschine");
        }
    }
}
