using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarBiddingPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class WinnerUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HighestBidderId",
                table: "Auctions",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HighestBidderId",
                table: "Auctions");
        }
    }
}
