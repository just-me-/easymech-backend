using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyMechBackend.Migrations
{
    public partial class reservationen_notiz_added_fabrikat_required_ondelete_cascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notiz",
                schema: "public",
                table: "MaschinenUebergabe",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Fabrikat",
                schema: "public",
                table: "Maschinentyp",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notiz",
                schema: "public",
                table: "MaschinenRuecknahme",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notiz",
                schema: "public",
                table: "MaschinenUebergabe");

            migrationBuilder.DropColumn(
                name: "Notiz",
                schema: "public",
                table: "MaschinenRuecknahme");

            migrationBuilder.AlterColumn<string>(
                name: "Fabrikat",
                schema: "public",
                table: "Maschinentyp",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128);
        }
    }
}
