using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemovedShowEpisodeDurationProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EpisodeDuration",
                table: "Shows");

            migrationBuilder.DropColumn(
                name: "NumberOfEpisodes",
                table: "Shows");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EpisodeDuration",
                table: "Shows",
                type: "datetime2",
                nullable: true,
                comment: "tv series episode duration");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfEpisodes",
                table: "Shows",
                type: "int",
                nullable: true,
                comment: "tv series number of episodes");
        }
    }
}
