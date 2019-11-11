using Microsoft.EntityFrameworkCore.Migrations;

namespace Signing.System.Tcc.Data.Migrations
{
    public partial class DocumentAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocumentNumber",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentNumber",
                table: "Users");
        }
    }
}
