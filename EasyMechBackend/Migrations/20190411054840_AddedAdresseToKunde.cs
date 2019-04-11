using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyMechBackend.Migrations
{
    public partial class AddedAdresseToKunde : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "public",
                table: "Kunden",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "Adresse",
                schema: "public",
                table: "Kunden",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                schema: "public",
                table: "Kunden",
                type: "xid",
                nullable: false,
                defaultValue: 0u);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adresse",
                schema: "public",
                table: "Kunden");

            migrationBuilder.DropColumn(
                name: "xmin",
                schema: "public",
                table: "Kunden");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "public",
                table: "Kunden",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool));
        }
    }
}
