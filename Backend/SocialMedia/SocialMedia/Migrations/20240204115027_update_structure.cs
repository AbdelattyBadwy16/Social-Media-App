using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMedia.Migrations
{
    /// <inheritdoc />
    public partial class update_structure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Angry",
                table: "posts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Haha",
                table: "posts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sads",
                table: "posts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Wow",
                table: "posts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "likes",
                table: "posts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "loves",
                table: "posts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    likes = table.Column<int>(type: "int", nullable: true),
                    loves = table.Column<int>(type: "int", nullable: true),
                    Sads = table.Column<int>(type: "int", nullable: true),
                    Haha = table.Column<int>(type: "int", nullable: true),
                    Angry = table.Column<int>(type: "int", nullable: true),
                    Wow = table.Column<int>(type: "int", nullable: true),
                    PostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_posts_PostId",
                        column: x => x.PostId,
                        principalTable: "posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_PostId",
                table: "Comment",
                column: "PostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropColumn(
                name: "Angry",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "Haha",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "Sads",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "Wow",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "likes",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "loves",
                table: "posts");
        }
    }
}
