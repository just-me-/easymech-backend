using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyMechBackend.Migrations
{
    public partial class nonnulldates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Startdatum",
                schema: "public",
                table: "Reservationen",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Enddatum",
                schema: "public",
                table: "Reservationen",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Datum",
                schema: "public",
                table: "MaschinenUebergabe",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Datum",
                schema: "public",
                table: "MaschinenRuecknahme",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Startdatum",
                schema: "public",
                table: "Reservationen",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Enddatum",
                schema: "public",
                table: "Reservationen",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Datum",
                schema: "public",
                table: "MaschinenUebergabe",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Datum",
                schema: "public",
                table: "MaschinenRuecknahme",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
