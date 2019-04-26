using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EasyMechBackend.Migrations
{
    public partial class Reservation_und_handout_and_return_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaktionen_Kunden_KundenId",
                schema: "public",
                table: "Transaktionen");

            migrationBuilder.AlterColumn<long>(
                name: "KundenId",
                schema: "public",
                table: "Transaktionen",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.CreateTable(
                name: "Reservationen",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Standort = table.Column<string>(maxLength: 256, nullable: true),
                    Startdatum = table.Column<DateTime>(nullable: false),
                    Enddatum = table.Column<DateTime>(nullable: false),
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

            migrationBuilder.CreateTable(
                name: "FahrzeugRuecknahme",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Datum = table.Column<DateTime>(nullable: false),
                    UebergabeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FahrzeugRuecknahme", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FahrzeugRuecknahme_FahrzeugUebergabe_UebergabeId",
                        column: x => x.UebergabeId,
                        principalSchema: "public",
                        principalTable: "FahrzeugUebergabe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FahrzeugRuecknahme_UebergabeId",
                schema: "public",
                table: "FahrzeugRuecknahme",
                column: "UebergabeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FahrzeugUebergabe_ReservationsId",
                schema: "public",
                table: "FahrzeugUebergabe",
                column: "ReservationsId",
                unique: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Transaktionen_Kunden_KundenId",
                schema: "public",
                table: "Transaktionen",
                column: "KundenId",
                principalSchema: "public",
                principalTable: "Kunden",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaktionen_Kunden_KundenId",
                schema: "public",
                table: "Transaktionen");

            migrationBuilder.DropTable(
                name: "FahrzeugRuecknahme",
                schema: "public");

            migrationBuilder.DropTable(
                name: "FahrzeugUebergabe",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Reservationen",
                schema: "public");

            migrationBuilder.AlterColumn<long>(
                name: "KundenId",
                schema: "public",
                table: "Transaktionen",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaktionen_Kunden_KundenId",
                schema: "public",
                table: "Transaktionen",
                column: "KundenId",
                principalSchema: "public",
                principalTable: "Kunden",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
