using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repair.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreatedatabase2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_Customers_CustomerId",
                table: "Repairs");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Repairs_CustomerId",
                table: "Repairs");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Repairs");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Repairs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Repairs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Repairs",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Repairs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Repairs");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Repairs");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Repairs");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Repairs");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "Repairs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_CustomerId",
                table: "Repairs",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_Customers_CustomerId",
                table: "Repairs",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
