using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyMechBackend.Migrations
{
    public partial class reservationReturnUmgehaengtIstSonstBehindert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaschinenRuecknahme_MaschinenUebergabe_MaschinenUebergabeId",
                schema: "public",
                table: "MaschinenRuecknahme");

            migrationBuilder.RenameColumn(
                name: "MaschinenUebergabeId",
                schema: "public",
                table: "MaschinenRuecknahme",
                newName: "ReservationsId");

            migrationBuilder.RenameIndex(
                name: "IX_MaschinenRuecknahme_MaschinenUebergabeId",
                schema: "public",
                table: "MaschinenRuecknahme",
                newName: "IX_MaschinenRuecknahme_ReservationsId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaschinenRuecknahme_Reservationen_ReservationsId",
                schema: "public",
                table: "MaschinenRuecknahme",
                column: "ReservationsId",
                principalSchema: "public",
                principalTable: "Reservationen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaschinenRuecknahme_Reservationen_ReservationsId",
                schema: "public",
                table: "MaschinenRuecknahme");

            migrationBuilder.RenameColumn(
                name: "ReservationsId",
                schema: "public",
                table: "MaschinenRuecknahme",
                newName: "MaschinenUebergabeId");

            migrationBuilder.RenameIndex(
                name: "IX_MaschinenRuecknahme_ReservationsId",
                schema: "public",
                table: "MaschinenRuecknahme",
                newName: "IX_MaschinenRuecknahme_MaschinenUebergabeId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaschinenRuecknahme_MaschinenUebergabe_MaschinenUebergabeId",
                schema: "public",
                table: "MaschinenRuecknahme",
                column: "MaschinenUebergabeId",
                principalSchema: "public",
                principalTable: "MaschinenUebergabe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
