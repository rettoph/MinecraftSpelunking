using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinecraftSpelunking.Domain.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionNormalizedColumnToJavaServers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescriptionNormalized",
                table: "JavaServers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionNormalized",
                table: "JavaServers");
        }
    }
}
