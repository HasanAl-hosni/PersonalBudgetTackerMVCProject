using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalBudgetTackerMVCProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class titleForCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Category",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Category");
        }
    }
}
