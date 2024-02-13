using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MinecraftSpelunking.Domain.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddMinecraftTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddressBlocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Network = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressBlocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModPackData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VersionId = table.Column<int>(type: "int", nullable: false),
                    IsMetadata = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModPackData", x => x.Id);
                    table.UniqueConstraint("AK_ModPackData_Name_Version", x => new { x.Name, x.Version });
                });

            migrationBuilder.CreateTable(
                name: "Mods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mods", x => x.Id);
                    table.UniqueConstraint("AK_Mods_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "ModTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModTypes", x => x.Id);
                    table.UniqueConstraint("AK_ModTypes_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "ReservedAddressBlocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Network = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservedAddressBlocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServerIcons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hash = table.Column<int>(type: "int", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerIcons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AddressBlockAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlockId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressBlockAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AddressBlockAssignments_AddressBlocks_BlockId",
                        column: x => x.BlockId,
                        principalTable: "AddressBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddressBlockAssignments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModVersions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModId = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModVersions", x => x.Id);
                    table.UniqueConstraint("AK_ModVersions_ModId_Version", x => new { x.ModId, x.Version });
                    table.ForeignKey(
                        name: "FK_ModVersions_Mods_ModId",
                        column: x => x.ModId,
                        principalTable: "Mods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JavaServers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Host = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Port = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlayersOnline = table.Column<int>(type: "int", nullable: false),
                    PlayersMax = table.Column<int>(type: "int", nullable: false),
                    PlayersSample = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionNormalized = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModTypeId = table.Column<int>(type: "int", nullable: true),
                    ModPackDataId = table.Column<int>(type: "int", nullable: true),
                    IconId = table.Column<int>(type: "int", nullable: true),
                    AddressBlockId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastOnlineAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JavaServers", x => x.Id);
                    table.UniqueConstraint("AK_JavaServers_Host_Port", x => new { x.Host, x.Port });
                    table.ForeignKey(
                        name: "FK_JavaServers_AddressBlocks_AddressBlockId",
                        column: x => x.AddressBlockId,
                        principalTable: "AddressBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JavaServers_ModPackData_ModPackDataId",
                        column: x => x.ModPackDataId,
                        principalTable: "ModPackData",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JavaServers_ModTypes_ModTypeId",
                        column: x => x.ModTypeId,
                        principalTable: "ModTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JavaServers_ServerIcons_IconId",
                        column: x => x.IconId,
                        principalTable: "ServerIcons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JavaServerModVersion",
                columns: table => new
                {
                    JavaServersId = table.Column<int>(type: "int", nullable: false),
                    ModVersionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JavaServerModVersion", x => new { x.JavaServersId, x.ModVersionsId });
                    table.ForeignKey(
                        name: "FK_JavaServerModVersion_JavaServers_JavaServersId",
                        column: x => x.JavaServersId,
                        principalTable: "JavaServers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JavaServerModVersion_ModVersions_ModVersionsId",
                        column: x => x.ModVersionsId,
                        principalTable: "ModVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ReservedAddressBlocks",
                columns: new[] { "Id", "CreatedAt", "ModifiedAt", "Network" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0.0.0.0/8" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "10.0.0.0/8" },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "100.64.0.0/10" },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "127.0.0.0/8" },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "169.254.0.0/16" },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "172.16.0.0/12" },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "192.0.0.0/24" },
                    { 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "192.0.2.0/24" },
                    { 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "192.88.99.0/24" },
                    { 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "192.168.0.0/16" },
                    { 11, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "198.18.0.0/15" },
                    { 12, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "198.51.100.0/24" },
                    { 13, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "203.0.113.0/24" },
                    { 14, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "224.0.0.0/4" },
                    { 15, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "240.0.0.0/4" },
                    { 16, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "255.255.255.255/32" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddressBlockAssignments_BlockId",
                table: "AddressBlockAssignments",
                column: "BlockId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressBlockAssignments_UserId",
                table: "AddressBlockAssignments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_JavaServerModVersion_ModVersionsId",
                table: "JavaServerModVersion",
                column: "ModVersionsId");

            migrationBuilder.CreateIndex(
                name: "IX_JavaServers_AddressBlockId",
                table: "JavaServers",
                column: "AddressBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_JavaServers_IconId",
                table: "JavaServers",
                column: "IconId");

            migrationBuilder.CreateIndex(
                name: "IX_JavaServers_ModPackDataId",
                table: "JavaServers",
                column: "ModPackDataId");

            migrationBuilder.CreateIndex(
                name: "IX_JavaServers_ModTypeId",
                table: "JavaServers",
                column: "ModTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddressBlockAssignments");

            migrationBuilder.DropTable(
                name: "JavaServerModVersion");

            migrationBuilder.DropTable(
                name: "ReservedAddressBlocks");

            migrationBuilder.DropTable(
                name: "JavaServers");

            migrationBuilder.DropTable(
                name: "ModVersions");

            migrationBuilder.DropTable(
                name: "AddressBlocks");

            migrationBuilder.DropTable(
                name: "ModPackData");

            migrationBuilder.DropTable(
                name: "ModTypes");

            migrationBuilder.DropTable(
                name: "ServerIcons");

            migrationBuilder.DropTable(
                name: "Mods");
        }
    }
}
