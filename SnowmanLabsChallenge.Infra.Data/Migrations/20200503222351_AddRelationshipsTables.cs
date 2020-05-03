using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SnowmanLabsChallenge.Infra.Data.Migrations
{
    public partial class AddRelationshipsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vote",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uuid = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    TouristSpotId = table.Column<int>(nullable: false),
                    Up = table.Column<bool>(nullable: false),
                    Down = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vote_TouristSpot_TouristSpotId",
                        column: x => x.TouristSpotId,
                        principalTable: "TouristSpot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vote_TouristSpotId",
                table: "Vote",
                column: "TouristSpotId");

            migrationBuilder.CreateIndex(
                name: "IX_Vote_Uuid",
                table: "Vote",
                column: "Uuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vote");
        }
    }
}
