using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EasyMechBackend.Migrations
{
    public partial class Service_Construct_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "ServiceDurchfuehrung",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    GeplanterServiceId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceDurchfuehrung", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceDurchfuehrung_GeplanterService_GeplanterServiceId",
                        column: x => x.GeplanterServiceId,
                        principalSchema: "public",
                        principalTable: "GeplanterService",
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
                    ServiceDurchfuehrungId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arbeitsschritt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Arbeitsschritt_ServiceDurchfuehrung_ServiceDurchfuehrungId",
                        column: x => x.ServiceDurchfuehrungId,
                        principalSchema: "public",
                        principalTable: "ServiceDurchfuehrung",
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
                    ServiceDurchfuehrungId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materialposten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Materialposten_ServiceDurchfuehrung_ServiceDurchfuehrungId",
                        column: x => x.ServiceDurchfuehrungId,
                        principalSchema: "public",
                        principalTable: "ServiceDurchfuehrung",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Arbeitsschritt_ServiceDurchfuehrungId",
                schema: "public",
                table: "Arbeitsschritt",
                column: "ServiceDurchfuehrungId");

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
                name: "IX_Materialposten_ServiceDurchfuehrungId",
                schema: "public",
                table: "Materialposten",
                column: "ServiceDurchfuehrungId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceDurchfuehrung_GeplanterServiceId",
                schema: "public",
                table: "ServiceDurchfuehrung",
                column: "GeplanterServiceId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Arbeitsschritt",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Materialposten",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ServiceDurchfuehrung",
                schema: "public");

            migrationBuilder.DropTable(
                name: "GeplanterService",
                schema: "public");
        }
    }
}
