using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinecraftSpelunking.Domain.Database.Migrations
{
    /// <inheritdoc />
    public partial class FixMispelledModPackColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ModPackData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VersionId = table.Column<int>(type: "int", nullable: false),
                    IsMetadata = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModPackData", x => x.Id);
                    table.UniqueConstraint("AK_ModPackData_ProjectId_VersionId", x => new { x.ProjectId, x.VersionId });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModPackData");
        }
    }
}
