using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizApi.Domain.Migrations
{
    /// <inheritdoc />
    public partial class test23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestResults_TestVariants_TestVariantId1",
                table: "TestResults");

            migrationBuilder.DropIndex(
                name: "IX_TestResults_TestVariantId1",
                table: "TestResults");

            migrationBuilder.DropColumn(
                name: "TestVariantId1",
                table: "TestResults");

            migrationBuilder.AlterColumn<int>(
                name: "TestVariantId",
                table: "TestResults",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.CreateIndex(
                name: "IX_TestResults_TestVariantId",
                table: "TestResults",
                column: "TestVariantId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestResults_TestVariants_TestVariantId",
                table: "TestResults",
                column: "TestVariantId",
                principalTable: "TestVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestResults_TestVariants_TestVariantId",
                table: "TestResults");

            migrationBuilder.DropIndex(
                name: "IX_TestResults_TestVariantId",
                table: "TestResults");

            migrationBuilder.AlterColumn<string>(
                name: "TestVariantId",
                table: "TestResults",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TestVariantId1",
                table: "TestResults",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TestResults_TestVariantId1",
                table: "TestResults",
                column: "TestVariantId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TestResults_TestVariants_TestVariantId1",
                table: "TestResults",
                column: "TestVariantId1",
                principalTable: "TestVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
