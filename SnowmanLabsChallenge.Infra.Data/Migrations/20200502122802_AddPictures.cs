using Microsoft.EntityFrameworkCore.Migrations;

namespace SnowmanLabsChallenge.Infra.Data.Migrations
{
    public partial class AddPictures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TouristSpotId",
                table: "Picture",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Picture",
                type: "varchar(2047)",
                maxLength: 2047,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Picture_TouristSpotId",
                table: "Picture",
                column: "TouristSpotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Picture_TouristSpot_TouristSpotId",
                table: "Picture",
                column: "TouristSpotId",
                principalTable: "TouristSpot",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Picture_TouristSpot_TouristSpotId",
                table: "Picture");

            migrationBuilder.DropIndex(
                name: "IX_Picture_TouristSpotId",
                table: "Picture");

            migrationBuilder.DropColumn(
                name: "TouristSpotId",
                table: "Picture");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Picture");
        }
    }
}
