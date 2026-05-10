using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIADAL.Migrations
{
    /// <inheritdoc />
    public partial class AddTemarioToProgram : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "temario",
                table: "programs",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "temario",
                table: "programs");
        }
    }
}
