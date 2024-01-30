using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinecraftSpelunking.Domain.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddModPackDataColumnToJavaServers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModPackDataId",
                table: "JavaServers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JavaServers_ModPackDataId",
                table: "JavaServers",
                column: "ModPackDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_JavaServers_ModPackData_ModPackDataId",
                table: "JavaServers",
                column: "ModPackDataId",
                principalTable: "ModPackData",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JavaServers_ModPackData_ModPackDataId",
                table: "JavaServers");

            migrationBuilder.DropIndex(
                name: "IX_JavaServers_ModPackDataId",
                table: "JavaServers");

            migrationBuilder.DropColumn(
                name: "ModPackDataId",
                table: "JavaServers");
        }
    }
}
