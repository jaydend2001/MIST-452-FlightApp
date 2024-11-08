using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SP24MVCDonham.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedAirlineLogo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "LogoBytes",
                table: "Airlines",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoBytes",
                table: "Airlines");
        }
    }
}
