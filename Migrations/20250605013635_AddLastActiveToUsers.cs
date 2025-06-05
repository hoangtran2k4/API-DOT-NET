using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_DOTNET.Migrations
{
    /// <inheritdoc />
    public partial class AddLastActiveToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastActive",
                schema: "OMS",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastActive",
                schema: "OMS",
                table: "Users");
        }
    }
}
