using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DurationTypesChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EpisodeDuration",
                table: "Shows");

            migrationBuilder.AddColumn<int>(
                name: "EpisodeDuration",
                table: "Shows",
                type: "int",
                nullable: true,
                comment: "tv series episode duration");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Shows");

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Shows",
                type: "int",
                nullable: true,
                comment: "movie duration");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EpisodeDuration",
                table: "Shows");

            migrationBuilder.AddColumn<DateTime>(
                name: "EpisodeDuration",
                table: "Shows",
                type: "datetime2",
                nullable: true,
                comment: "tv series episode duration");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Shows");

            migrationBuilder.AddColumn<DateTime>(
                name: "Duration",
                table: "Shows",
                type: "datetime2",
                nullable: true,
                comment: "movie duration");
        }
    }
}
