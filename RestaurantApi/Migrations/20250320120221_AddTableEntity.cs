using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantApi.Migrations
{
    /// <inheritdoc />
    public partial class AddTableEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Tables");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Tables",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Tables");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Tables",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
