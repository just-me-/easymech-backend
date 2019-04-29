using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EasyMechBackend.Migrations
{
    public partial class Rename_Fz2Maschine_Complete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maschine_Fahrzeugtyp_MaschinenTypId",
                schema: "public",
                table: "Maschine");

            migrationBuilder.DropForeignKey(
                name: "FK_MaschinenRuecknahme_FahrzeugUebergabe_UebergabeId",
                schema: "public",
                table: "MaschinenRuecknahme");

            migrationBuilder.DropTable(
                name: "Fahrzeugtyp",
                schema: "public");

            migrationBuilder.DropTable(
                name: "FahrzeugUebergabe",
                schema: "public");

            migrationBuilder.RenameColumn(
                name: "UebergabeId",
                schema: "public",
                table: "MaschinenRuecknahme",
                newName: "MaschinenUebergabeId");

            migrationBuilder.RenameIndex(
                name: "IX_MaschinenRuecknahme_UebergabeId",
                schema: "public",
                table: "MaschinenRuecknahme",
                newName: "IX_MaschinenRuecknahme_MaschinenUebergabeId");

            migrationBuilder.CreateTable(
                name: "Maschinentyp",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Fabrikat = table.Column<string>(maxLength: 128, nullable: true),
                    Motortyp = table.Column<string>(maxLength: 128, nullable: true),
                    Nutzlast = table.Column<int>(nullable: false),
                    Hubkraft = table.Column<int>(nullable: false),
                    Hubhoehe = table.Column<int>(nullable: false),
                    Eigengewicht = table.Column<int>(nullable: false),
                    Maschinenhoehe = table.Column<int>(nullable: false),
                    Maschinenlaenge = table.Column<int>(nullable: false),
                    Maschinenbreite = table.Column<int>(nullable: false),
                    Pneugroesse = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maschinentyp", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaschinenUebergabe",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Datum = table.Column<DateTime>(nullable: false),
                    ReservationsId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaschinenUebergabe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaschinenUebergabe_Reservationen_ReservationsId",
                        column: x => x.ReservationsId,
                        principalSchema: "public",
                        principalTable: "Reservationen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaschinenUebergabe_ReservationsId",
                schema: "public",
                table: "MaschinenUebergabe",
                column: "ReservationsId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Maschine_Maschinentyp_MaschinenTypId",
                schema: "public",
                table: "Maschine",
                column: "MaschinenTypId",
                principalSchema: "public",
                principalTable: "Maschinentyp",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maschine_Maschinentyp_MaschinenTypId",
                schema: "public",
                table: "Maschine");

            migrationBuilder.DropForeignKey(
                name: "FK_MaschinenRuecknahme_MaschinenUebergabe_MaschinenUebergabeId",
                schema: "public",
                table: "MaschinenRuecknahme");

            migrationBuilder.DropTable(
                name: "Maschinentyp",
                schema: "public");

            migrationBuilder.DropTable(
                name: "MaschinenUebergabe",
                schema: "public");

            migrationBuilder.RenameColumn(
                name: "MaschinenUebergabeId",
                schema: "public",
                table: "MaschinenRuecknahme",
                newName: "UebergabeId");

            migrationBuilder.RenameIndex(
                name: "IX_MaschinenRuecknahme_MaschinenUebergabeId",
                schema: "public",
                table: "MaschinenRuecknahme",
                newName: "IX_MaschinenRuecknahme_UebergabeId");

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
                    Fahrzeughoehe = table.Column<int>(nullable: false),
                    Fahrzeuglaenge = table.Column<int>(nullable: false),
                    Hubhoehe = table.Column<int>(nullable: false),
                    Hubkraft = table.Column<int>(nullable: false),
                    Motortyp = table.Column<string>(maxLength: 128, nullable: true),
                    Nutzlast = table.Column<int>(nullable: false),
                    Pneugroesse = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fahrzeugtyp", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FahrzeugUebergabe",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Datum = table.Column<DateTime>(nullable: false),
                    ReservationsId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FahrzeugUebergabe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FahrzeugUebergabe_Reservationen_ReservationsId",
                        column: x => x.ReservationsId,
                        principalSchema: "public",
                        principalTable: "Reservationen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FahrzeugUebergabe_ReservationsId",
                schema: "public",
                table: "FahrzeugUebergabe",
                column: "ReservationsId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Maschine_Fahrzeugtyp_MaschinenTypId",
                schema: "public",
                table: "Maschine",
                column: "MaschinenTypId",
                principalSchema: "public",
                principalTable: "Fahrzeugtyp",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaschinenRuecknahme_FahrzeugUebergabe_UebergabeId",
                schema: "public",
                table: "MaschinenRuecknahme",
                column: "UebergabeId",
                principalSchema: "public",
                principalTable: "FahrzeugUebergabe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
