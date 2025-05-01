using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlaySpotApi.Migrations
{
    /// <inheritdoc />
    public partial class UsingImplicitTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationSports");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "LocationSports",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "integer", nullable: false),
                    SportId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationSports", x => new { x.LocationId, x.SportId });
                    table.ForeignKey(
                        name: "FK_LocationSports_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationSports_Sports_SportId",
                        column: x => x.SportId,
                        principalTable: "Sports",
                        principalColumn: "SportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocationSports_SportId",
                table: "LocationSports",
                column: "SportId");
        }
    }
}
