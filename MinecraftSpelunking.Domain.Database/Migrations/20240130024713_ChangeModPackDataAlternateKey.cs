using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinecraftSpelunking.Domain.Database.Migrations
{
    /// <inheritdoc />
    public partial class ChangeModPackDataAlternateKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_ModPackData_ProjectId_VersionId",
                table: "ModPackData");

            migrationBuilder.AlterColumn<string>(
                name: "Version",
                table: "ModPackData",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ModPackData",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ModPackData_Name_Version",
                table: "ModPackData",
                columns: new[] { "Name", "Version" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_ModPackData_Name_Version",
                table: "ModPackData");

            migrationBuilder.AlterColumn<string>(
                name: "Version",
                table: "ModPackData",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ModPackData",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ModPackData_ProjectId_VersionId",
                table: "ModPackData",
                columns: new[] { "ProjectId", "VersionId" });
        }
    }
}
