using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlaySpotApi.Migrations
{
    /// <inheritdoc />
    public partial class AddRequiredToFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "dateTime",
                table: "LocationActivities",
                newName: "DateTime");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Sports",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "LocationActivities",
                newName: "dateTime");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Sports",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
