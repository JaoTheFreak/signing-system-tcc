using Microsoft.EntityFrameworkCore.Migrations;

namespace Signing.System.Tcc.Data.Migrations
{
    public partial class UpdatedAtColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAte",
                table: "Records",
                newName: "UpdatedAt");

            migrationBuilder.AlterColumn<long>(
                name: "MidiaSizeBytes",
                table: "Records",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Records",
                newName: "UpdatedAte");

            migrationBuilder.AlterColumn<int>(
                name: "MidiaSizeBytes",
                table: "Records",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
