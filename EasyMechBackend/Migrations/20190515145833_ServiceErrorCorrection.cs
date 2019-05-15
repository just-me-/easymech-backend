using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EasyMechBackend.Migrations
{
    public partial class ServiceErrorCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "Kunden",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Firma = table.Column<string>(maxLength: 128, nullable: false),
                    Vorname = table.Column<string>(maxLength: 128, nullable: true),
                    Nachname = table.Column<string>(maxLength: 128, nullable: true),
                    Adresse = table.Column<string>(maxLength: 128, nullable: true),
                    PLZ = table.Column<string>(maxLength: 128, nullable: true),
                    Ort = table.Column<string>(maxLength: 128, nullable: true),
                    Email = table.Column<string>(maxLength: 128, nullable: true),
                    Telefon = table.Column<string>(maxLength: 128, nullable: true),
                    Notiz = table.Column<string>(nullable: true),
                    IstAktiv = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kunden", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Maschinentyp",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Fabrikat = table.Column<string>(maxLength: 128, nullable: false),
                    Motortyp = table.Column<string>(maxLength: 128, nullable: true),
                    Nutzlast = table.Column<int>(nullable: true),
                    Hubkraft = table.Column<int>(nullable: true),
                    Hubhoehe = table.Column<int>(nullable: true),
                    Eigengewicht = table.Column<int>(nullable: true),
                    Maschinenhoehe = table.Column<int>(nullable: true),
                    Maschinenlaenge = table.Column<int>(nullable: true),
                    Maschinenbreite = table.Column<int>(nullable: true),
                    Pneugroesse = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maschinentyp", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Maschine",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Seriennummer = table.Column<string>(maxLength: 128, nullable: true),
                    Mastnummer = table.Column<string>(maxLength: 128, nullable: true),
                    Motorennummer = table.Column<string>(maxLength: 128, nullable: true),
                    Betriebsdauer = table.Column<int>(nullable: true),
                    Jahrgang = table.Column<int>(nullable: true),
                    Notiz = table.Column<string>(nullable: true),
                    IstAktiv = table.Column<bool>(nullable: false),
                    BesitzerId = table.Column<long>(nullable: false),
                    MaschinentypId = table.Column<long>(nullable: false)
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Maschine_Maschinentyp_MaschinentypId",
                        column: x => x.MaschinentypId,
                        principalSchema: "public",
                        principalTable: "Maschinentyp",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GeplanterService",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Bezeichnung = table.Column<string>(maxLength: 128, nullable: true),
                    Beginn = table.Column<DateTime>(nullable: false),
                    Ende = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    MaschinenId = table.Column<long>(nullable: false),
                    KundenId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeplanterService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeplanterService_Kunden_KundenId",
                        column: x => x.KundenId,
                        principalSchema: "public",
                        principalTable: "Kunden",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GeplanterService_Maschine_MaschinenId",
                        column: x => x.MaschinenId,
                        principalSchema: "public",
                        principalTable: "Maschine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservationen",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Standort = table.Column<string>(maxLength: 256, nullable: true),
                    Startdatum = table.Column<DateTime>(nullable: true),
                    Enddatum = table.Column<DateTime>(nullable: true),
                    MaschinenId = table.Column<long>(nullable: false),
                    KundenId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservationen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservationen_Kunden_KundenId",
                        column: x => x.KundenId,
                        principalSchema: "public",
                        principalTable: "Kunden",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservationen_Maschine_MaschinenId",
                        column: x => x.MaschinenId,
                        principalSchema: "public",
                        principalTable: "Maschine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transaktionen",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Preis = table.Column<double>(nullable: false),
                    Typ = table.Column<int>(nullable: false),
                    Datum = table.Column<DateTime>(nullable: true),
                    MaschinenId = table.Column<long>(nullable: false),
                    KundenId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaktionen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaktionen_Kunden_KundenId",
                        column: x => x.KundenId,
                        principalSchema: "public",
                        principalTable: "Kunden",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaktionen_Maschine_MaschinenId",
                        column: x => x.MaschinenId,
                        principalSchema: "public",
                        principalTable: "Maschine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Arbeitsschritt",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Bezeichnung = table.Column<string>(maxLength: 256, nullable: true),
                    Stundenansatz = table.Column<double>(nullable: true),
                    Arbeitsstunden = table.Column<double>(nullable: true),
                    ServiceId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arbeitsschritt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Arbeitsschritt_GeplanterService_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "public",
                        principalTable: "GeplanterService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Materialposten",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Stueckpreis = table.Column<double>(nullable: false),
                    Anzahl = table.Column<int>(nullable: false),
                    Bezeichnung = table.Column<string>(maxLength: 256, nullable: true),
                    ServiceId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materialposten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Materialposten_GeplanterService_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "public",
                        principalTable: "GeplanterService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaschinenRuecknahme",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Datum = table.Column<DateTime>(nullable: false),
                    Notiz = table.Column<string>(nullable: true),
                    ReservationsId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaschinenRuecknahme", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaschinenRuecknahme_Reservationen_ReservationsId",
                        column: x => x.ReservationsId,
                        principalSchema: "public",
                        principalTable: "Reservationen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaschinenUebergabe",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Datum = table.Column<DateTime>(nullable: false),
                    Notiz = table.Column<string>(nullable: true),
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
                name: "IX_Arbeitsschritt_ServiceId",
                schema: "public",
                table: "Arbeitsschritt",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_GeplanterService_KundenId",
                schema: "public",
                table: "GeplanterService",
                column: "KundenId");

            migrationBuilder.CreateIndex(
                name: "IX_GeplanterService_MaschinenId",
                schema: "public",
                table: "GeplanterService",
                column: "MaschinenId");

            migrationBuilder.CreateIndex(
                name: "IX_Maschine_BesitzerId",
                schema: "public",
                table: "Maschine",
                column: "BesitzerId");

            migrationBuilder.CreateIndex(
                name: "IX_Maschine_MaschinentypId",
                schema: "public",
                table: "Maschine",
                column: "MaschinentypId");

            migrationBuilder.CreateIndex(
                name: "IX_MaschinenRuecknahme_ReservationsId",
                schema: "public",
                table: "MaschinenRuecknahme",
                column: "ReservationsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaschinenUebergabe_ReservationsId",
                schema: "public",
                table: "MaschinenUebergabe",
                column: "ReservationsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Materialposten_ServiceId",
                schema: "public",
                table: "Materialposten",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservationen_KundenId",
                schema: "public",
                table: "Reservationen",
                column: "KundenId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservationen_MaschinenId",
                schema: "public",
                table: "Reservationen",
                column: "MaschinenId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaktionen_KundenId",
                schema: "public",
                table: "Transaktionen",
                column: "KundenId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaktionen_MaschinenId",
                schema: "public",
                table: "Transaktionen",
                column: "MaschinenId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Arbeitsschritt",
                schema: "public");

            migrationBuilder.DropTable(
                name: "MaschinenRuecknahme",
                schema: "public");

            migrationBuilder.DropTable(
                name: "MaschinenUebergabe",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Materialposten",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Transaktionen",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Reservationen",
                schema: "public");

            migrationBuilder.DropTable(
                name: "GeplanterService",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Maschine",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Kunden",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Maschinentyp",
                schema: "public");
        }
    }
}
