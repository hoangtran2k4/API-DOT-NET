using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_DOTNET.Migrations
{
    /// <inheritdoc />
    public partial class AddIsOnlineToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOnline",
                schema: "OMS",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOnline",
                schema: "OMS",
                table: "Users");
        }
    }
}
