using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemovedTableShowType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shows_ShowTypes_ShowTypeId",
                table: "Shows");

            migrationBuilder.DropTable(
                name: "ShowTypes");

            migrationBuilder.DropIndex(
                name: "IX_Shows_ShowTypeId",
                table: "Shows");

            migrationBuilder.DropColumn(
                name: "ShowTypeId",
                table: "Shows");

            migrationBuilder.AddColumn<int>(
                name: "ShowType",
                table: "Shows",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowType",
                table: "Shows");

            migrationBuilder.AddColumn<int>(
                name: "ShowTypeId",
                table: "Shows",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "show type id");

            migrationBuilder.CreateTable(
                name: "ShowTypes",
                columns: table => new
                {
                    ShowTypeId = table.Column<int>(type: "int", nullable: false, comment: "show type id")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "show type name")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShowTypes", x => x.ShowTypeId);
                },
                comment: "show type table");

            migrationBuilder.CreateIndex(
                name: "IX_Shows_ShowTypeId",
                table: "Shows",
                column: "ShowTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shows_ShowTypes_ShowTypeId",
                table: "Shows",
                column: "ShowTypeId",
                principalTable: "ShowTypes",
                principalColumn: "ShowTypeId");
        }
    }
}
