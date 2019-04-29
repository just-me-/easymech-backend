using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyMechBackend.Migrations
{
    public partial class SetMostFieldsToOptionalButMaschineMustHaveCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maschine_Kunden_BesitzerId",
                schema: "public",
                table: "Maschine");

            migrationBuilder.DropForeignKey(
                name: "FK_Maschine_Maschinentyp_MaschinenTypId",
                schema: "public",
                table: "Maschine");

            migrationBuilder.RenameColumn(
                name: "MaschinenTypId",
                schema: "public",
                table: "Maschine",
                newName: "MaschinentypId");

            migrationBuilder.RenameIndex(
                name: "IX_Maschine_MaschinenTypId",
                schema: "public",
                table: "Maschine",
                newName: "IX_Maschine_MaschinentypId");

            migrationBuilder.AlterColumn<int>(
                name: "Pneugroesse",
                schema: "public",
                table: "Maschinentyp",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Nutzlast",
                schema: "public",
                table: "Maschinentyp",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Maschinenlaenge",
                schema: "public",
                table: "Maschinentyp",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Maschinenhoehe",
                schema: "public",
                table: "Maschinentyp",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Maschinenbreite",
                schema: "public",
                table: "Maschinentyp",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Hubkraft",
                schema: "public",
                table: "Maschinentyp",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Hubhoehe",
                schema: "public",
                table: "Maschinentyp",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Eigengewicht",
                schema: "public",
                table: "Maschinentyp",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Jahrgang",
                schema: "public",
                table: "Maschine",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Betriebsdauer",
                schema: "public",
                table: "Maschine",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "BesitzerId",
                schema: "public",
                table: "Maschine",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Maschine_Kunden_BesitzerId",
                schema: "public",
                table: "Maschine",
                column: "BesitzerId",
                principalSchema: "public",
                principalTable: "Kunden",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Maschine_Maschinentyp_MaschinentypId",
                schema: "public",
                table: "Maschine",
                column: "MaschinentypId",
                principalSchema: "public",
                principalTable: "Maschinentyp",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maschine_Kunden_BesitzerId",
                schema: "public",
                table: "Maschine");

            migrationBuilder.DropForeignKey(
                name: "FK_Maschine_Maschinentyp_MaschinentypId",
                schema: "public",
                table: "Maschine");

            migrationBuilder.RenameColumn(
                name: "MaschinentypId",
                schema: "public",
                table: "Maschine",
                newName: "MaschinenTypId");

            migrationBuilder.RenameIndex(
                name: "IX_Maschine_MaschinentypId",
                schema: "public",
                table: "Maschine",
                newName: "IX_Maschine_MaschinenTypId");

            migrationBuilder.AlterColumn<int>(
                name: "Pneugroesse",
                schema: "public",
                table: "Maschinentyp",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Nutzlast",
                schema: "public",
                table: "Maschinentyp",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Maschinenlaenge",
                schema: "public",
                table: "Maschinentyp",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Maschinenhoehe",
                schema: "public",
                table: "Maschinentyp",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Maschinenbreite",
                schema: "public",
                table: "Maschinentyp",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Hubkraft",
                schema: "public",
                table: "Maschinentyp",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Hubhoehe",
                schema: "public",
                table: "Maschinentyp",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Eigengewicht",
                schema: "public",
                table: "Maschinentyp",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Jahrgang",
                schema: "public",
                table: "Maschine",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Betriebsdauer",
                schema: "public",
                table: "Maschine",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "BesitzerId",
                schema: "public",
                table: "Maschine",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Maschine_Kunden_BesitzerId",
                schema: "public",
                table: "Maschine",
                column: "BesitzerId",
                principalSchema: "public",
                principalTable: "Kunden",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Maschine_Maschinentyp_MaschinenTypId",
                schema: "public",
                table: "Maschine",
                column: "MaschinenTypId",
                principalSchema: "public",
                principalTable: "Maschinentyp",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
