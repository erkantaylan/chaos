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

            // Assign sequential PostNumber to existing rows before adding unique index
            migrationBuilder.Sql(
                """
                WITH numbered AS (
                    SELECT "Id", ROW_NUMBER() OVER (ORDER BY "CreationTime") AS rn
                    FROM "AppBlogPosts"
                )
                UPDATE "AppBlogPosts" SET "PostNumber" = numbered.rn
                FROM numbered WHERE "AppBlogPosts"."Id" = numbered."Id";

                SELECT setval(pg_get_serial_sequence('"AppBlogPosts"', 'PostNumber'),
                    GREATEST((SELECT MAX("PostNumber") FROM "AppBlogPosts"), 1));
                """);

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
