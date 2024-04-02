using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ShowPhotoNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhotoId",
                table: "Shows",
                type: "nvarchar(450)",
                nullable: true,
                comment: "show photo id",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldComment: "show photo id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhotoId",
                table: "Shows",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                comment: "show photo id",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true,
                oldComment: "show photo id");
        }
    }
}
