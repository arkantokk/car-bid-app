using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarBiddingPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSellerIdToAuction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SellerId",
                table: "Auctions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "Auctions",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Auctions");
        }
    }
}
