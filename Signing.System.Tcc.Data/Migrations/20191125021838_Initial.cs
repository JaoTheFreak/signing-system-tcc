using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Signing.System.Tcc.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "User",
                startValue: 12L);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValueSql: "nextval('\"User\"')"),
                    PasswordHash = table.Column<string>(nullable: true),
                    DocumentNumber = table.Column<string>(nullable: true),
                    Salt = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "NOW()"),
                    UpdatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Records",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(nullable: false),
                    TransactionHash = table.Column<string>(nullable: true),
                    TransactionFee = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    MidiaHash = table.Column<string>(nullable: true),
                    MidiaDescription = table.Column<string>(nullable: true),
                    MidiaName = table.Column<string>(nullable: true),
                    MidiaExtension = table.Column<string>(nullable: true),
                    MidiaResolution = table.Column<string>(nullable: true),
                    MidiaSizeBytes = table.Column<int>(nullable: false),
                    MidiaUrl = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "NOW()"),
                    UpdatedAte = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Records", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Records_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Records_UserId",
                table: "Records",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Records");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropSequence(
                name: "User");
        }
    }
}
