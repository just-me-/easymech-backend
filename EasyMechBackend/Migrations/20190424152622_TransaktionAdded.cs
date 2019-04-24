using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EasyMechBackend.Migrations
{
    public partial class TransaktionAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    KundenId = table.Column<long>(nullable: false)
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaktionen_Maschine_MaschinenId",
                        column: x => x.MaschinenId,
                        principalSchema: "public",
                        principalTable: "Maschine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "Transaktionen",
                schema: "public");
        }
    }
}
