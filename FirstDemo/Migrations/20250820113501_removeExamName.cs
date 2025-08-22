using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstDemo.Migrations
{
    /// <inheritdoc />
    public partial class removeExamName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExameName",
                table: "exames");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExameName",
                table: "exames",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
