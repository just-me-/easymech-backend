﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyMechBackend.Migrations
{
    public partial class ondeletebehaviors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maschine_Maschinentyp_MaschinentypId",
                schema: "public",
                table: "Maschine");

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
                name: "FK_Maschine_Maschinentyp_MaschinentypId",
                schema: "public",
                table: "Maschine");

            migrationBuilder.AddForeignKey(
                name: "FK_Maschine_Maschinentyp_MaschinentypId",
                schema: "public",
                table: "Maschine",
                column: "MaschinentypId",
                principalSchema: "public",
                principalTable: "Maschinentyp",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}