using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repair.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initialecreate6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedImages",
                table: "Repairs");

            migrationBuilder.DropColumn(
                name: "ReceivedImages",
                table: "Repairs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompletedImages",
                table: "Repairs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceivedImages",
                table: "Repairs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
