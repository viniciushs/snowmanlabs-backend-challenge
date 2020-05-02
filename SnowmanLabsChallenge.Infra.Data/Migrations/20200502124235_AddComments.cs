using Microsoft.EntityFrameworkCore.Migrations;

namespace SnowmanLabsChallenge.Infra.Data.Migrations
{
    public partial class AddComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Comment",
                type: "varchar(2047)",
                maxLength: 2047,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TouristSpotId",
                table: "Comment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Comment_TouristSpotId",
                table: "Comment",
                column: "TouristSpotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_TouristSpot_TouristSpotId",
                table: "Comment",
                column: "TouristSpotId",
                principalTable: "TouristSpot",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_TouristSpot_TouristSpotId",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_TouristSpotId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "TouristSpotId",
                table: "Comment");
        }
    }
}
