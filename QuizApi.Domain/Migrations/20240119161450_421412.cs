using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizApi.Domain.Migrations
{
    /// <inheritdoc />
    public partial class _421412 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TestVariantName",
                table: "TestResults",
                type: "longtext",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestVariantName",
                table: "TestResults");
        }
    }
}
