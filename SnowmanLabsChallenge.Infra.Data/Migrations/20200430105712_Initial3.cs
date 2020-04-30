using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;

namespace SnowmanLabsChallenge.Infra.Data.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Category");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Category",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            var seedScriptContent = File.ReadAllText("../SnowmanLabsChallenge.Infra.Data/Scripts/seed-data-up-script.sql");
            migrationBuilder.Sql(seedScriptContent);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Category");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Category",
                type: "nvarchar(max)",
                nullable: true);

            var seedScriptContent = File.ReadAllText("../SnowmanLabsChallenge.Infra.Data/Scripts/seed-data-down-script.sql");
            migrationBuilder.Sql(seedScriptContent);
        }
    }
}
