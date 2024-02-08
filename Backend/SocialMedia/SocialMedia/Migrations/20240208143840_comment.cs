using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMedia.Migrations
{
    /// <inheritdoc />
    public partial class comment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Angry",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "Haha",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "Sads",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "Wow",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "likes",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "loves",
                table: "Comment");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Comment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Comment");

            migrationBuilder.AddColumn<int>(
                name: "Angry",
                table: "Comment",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Haha",
                table: "Comment",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sads",
                table: "Comment",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Wow",
                table: "Comment",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "likes",
                table: "Comment",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "loves",
                table: "Comment",
                type: "int",
                nullable: true);
        }
    }
}
