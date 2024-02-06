using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMedia.Migrations
{
    /// <inheritdoc />
    public partial class update3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "photos");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "photos",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "photos");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "photos",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
