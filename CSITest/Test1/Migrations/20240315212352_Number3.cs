using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test1.Migrations
{
    /// <inheritdoc />
    public partial class Number3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "Numbers");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Numbers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Numbers");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "Numbers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
