using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SnowmanLabsChallenge.Infra.Data.Migrations
{
    public partial class AddUserIdIntoTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "TouristSpot",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Picture",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "TouristSpotId",
                table: "Favorite",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Favorite",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Comment",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_TouristSpotId",
                table: "Favorite",
                column: "TouristSpotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorite_TouristSpot_TouristSpotId",
                table: "Favorite",
                column: "TouristSpotId",
                principalTable: "TouristSpot",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorite_TouristSpot_TouristSpotId",
                table: "Favorite");

            migrationBuilder.DropIndex(
                name: "IX_Favorite_TouristSpotId",
                table: "Favorite");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "TouristSpot");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Picture");

            migrationBuilder.DropColumn(
                name: "TouristSpotId",
                table: "Favorite");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Favorite");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Comment");
        }
    }
}
