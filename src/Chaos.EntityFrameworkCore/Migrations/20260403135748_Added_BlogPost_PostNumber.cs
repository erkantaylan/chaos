using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Chaos.Migrations
{
    /// <inheritdoc />
    public partial class Added_BlogPost_PostNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostNumber",
                table: "AppBlogPosts",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_AppBlogPosts_PostNumber",
                table: "AppBlogPosts",
                column: "PostNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppBlogPosts_PostNumber",
                table: "AppBlogPosts");

            migrationBuilder.DropColumn(
                name: "PostNumber",
                table: "AppBlogPosts");
        }
    }
}
