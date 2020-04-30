using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace SnowmanLabsChallenge.Infra.Data.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Point>(
                name: "Location",
                table: "TouristSpot",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "geography",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Point>(
                name: "Location",
                table: "TouristSpot",
                type: "geography",
                nullable: true,
                oldClrType: typeof(Point));
        }
    }
}
