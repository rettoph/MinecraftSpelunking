using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinecraftSpelunking.Domain.Database.Migrations
{
    /// <inheritdoc />
    public partial class MakeModTypeNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JavaServers_ModTypes_ModTypeId",
                table: "JavaServers");

            migrationBuilder.DropForeignKey(
                name: "FK_ModVersions_Mod_ModId",
                table: "ModVersions");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Mod_Name",
                table: "Mod");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Mod",
                table: "Mod");

            migrationBuilder.RenameTable(
                name: "Mod",
                newName: "Mods");

            migrationBuilder.AlterColumn<int>(
                name: "ModTypeId",
                table: "JavaServers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Mods_Name",
                table: "Mods",
                column: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mods",
                table: "Mods",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JavaServers_ModTypes_ModTypeId",
                table: "JavaServers",
                column: "ModTypeId",
                principalTable: "ModTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ModVersions_Mods_ModId",
                table: "ModVersions",
                column: "ModId",
                principalTable: "Mods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JavaServers_ModTypes_ModTypeId",
                table: "JavaServers");

            migrationBuilder.DropForeignKey(
                name: "FK_ModVersions_Mods_ModId",
                table: "ModVersions");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Mods_Name",
                table: "Mods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Mods",
                table: "Mods");

            migrationBuilder.RenameTable(
                name: "Mods",
                newName: "Mod");

            migrationBuilder.AlterColumn<int>(
                name: "ModTypeId",
                table: "JavaServers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Mod_Name",
                table: "Mod",
                column: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mod",
                table: "Mod",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JavaServers_ModTypes_ModTypeId",
                table: "JavaServers",
                column: "ModTypeId",
                principalTable: "ModTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ModVersions_Mod_ModId",
                table: "ModVersions",
                column: "ModId",
                principalTable: "Mod",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
