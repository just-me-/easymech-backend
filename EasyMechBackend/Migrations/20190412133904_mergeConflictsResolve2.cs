using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EasyMechBackend.Migrations
{
    public partial class mergeConflictsResolve2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Maschine",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Fahrzeugtyp",
                schema: "public");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                schema: "public",
                table: "Kunden");

            migrationBuilder.AlterColumn<string>(
                name: "Vorname",
                schema: "public",
                table: "Kunden",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "PLZ",
                schema: "public",
                table: "Kunden",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Ort",
                schema: "public",
                table: "Kunden",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "Nachname",
                schema: "public",
                table: "Kunden",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "public",
                table: "Kunden",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "Firma",
                schema: "public",
                table: "Kunden",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "public",
                table: "Kunden",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AddColumn<string>(
                name: "Adresse",
                schema: "public",
                table: "Kunden",
                maxLength: 128,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adresse",
                schema: "public",
                table: "Kunden");

            migrationBuilder.AlterColumn<string>(
                name: "Vorname",
                schema: "public",
                table: "Kunden",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PLZ",
                schema: "public",
                table: "Kunden",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Ort",
                schema: "public",
                table: "Kunden",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nachname",
                schema: "public",
                table: "Kunden",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "public",
                table: "Kunden",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<string>(
                name: "Firma",
                schema: "public",
                table: "Kunden",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "public",
                table: "Kunden",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                schema: "public",
                table: "Kunden",
                rowVersion: true,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Fahrzeugtyp",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Eigengewicht = table.Column<int>(nullable: false),
                    Fabrikat = table.Column<string>(maxLength: 128, nullable: true),
                    Fahrzeugbreite = table.Column<int>(nullable: false),
                    Fahrzeughöhe = table.Column<int>(nullable: false),
                    Fahrzeuglänge = table.Column<int>(nullable: false),
                    Hubhöhe = table.Column<int>(nullable: false),
                    Hubkraft = table.Column<int>(nullable: false),
                    Jahrgang = table.Column<int>(nullable: false),
                    Motortyp = table.Column<string>(maxLength: 128, nullable: true),
                    Nutzlast = table.Column<int>(nullable: false),
                    Pneugrösse = table.Column<int>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true)
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
                    Bertriebsdauer = table.Column<int>(nullable: false),
                    BesitzerId = table.Column<long>(nullable: true),
                    IstAktiv = table.Column<bool>(nullable: false),
                    Mastnummer = table.Column<string>(maxLength: 128, nullable: true),
                    Motorennummer = table.Column<string>(maxLength: 128, nullable: true),
                    Seriennummer = table.Column<string>(maxLength: 128, nullable: true),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TypId = table.Column<long>(nullable: false)
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
    }
}
