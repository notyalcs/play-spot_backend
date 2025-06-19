using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlaySpotApi.Migrations
{
    /// <inheritdoc />
    public partial class FixedRelationshipForLocationsSports : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sports_Locations_LocationId",
                table: "Sports");

            migrationBuilder.DropIndex(
                name: "IX_Sports_LocationId",
                table: "Sports");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Sports");

            migrationBuilder.CreateTable(
                name: "LocationSport",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "integer", nullable: false),
                    SportsSportId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationSport", x => new { x.LocationId, x.SportsSportId });
                    table.ForeignKey(
                        name: "FK_LocationSport_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationSport_Sports_SportsSportId",
                        column: x => x.SportsSportId,
                        principalTable: "Sports",
                        principalColumn: "SportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocationSport_SportsSportId",
                table: "LocationSport",
                column: "SportsSportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationSport");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Sports",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sports_LocationId",
                table: "Sports",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sports_Locations_LocationId",
                table: "Sports",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "LocationId");
        }
    }
}
