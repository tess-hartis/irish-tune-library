using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TL.Data.Migrations
{
    public partial class enumValObj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TuneType",
                table: "Tunes",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "TuneKey",
                table: "Tunes",
                newName: "Key");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Tunes",
                newName: "TuneType");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "Tunes",
                newName: "TuneKey");
        }
    }
}
