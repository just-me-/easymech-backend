using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EasyMechBackend.Migrations
{
    public partial class Rename_Fz2Maschine_Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FahrzeugRuecknahme",
                schema: "public");

            migrationBuilder.CreateTable(
                name: "MaschinenRuecknahme",
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
                    table.PrimaryKey("PK_MaschinenRuecknahme", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaschinenRuecknahme_FahrzeugUebergabe_UebergabeId",
                        column: x => x.UebergabeId,
                        principalSchema: "public",
                        principalTable: "FahrzeugUebergabe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaschinenRuecknahme_UebergabeId",
                schema: "public",
                table: "MaschinenRuecknahme",
                column: "UebergabeId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaschinenRuecknahme",
                schema: "public");

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
        }
    }
}
