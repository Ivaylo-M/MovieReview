using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CountriesOfOrigin",
                columns: table => new
                {
                    CountryOfOriginId = table.Column<int>(type: "int", nullable: false, comment: "country of origin id")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "country of origin name")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountriesOfOrigin", x => x.CountryOfOriginId);
                },
                comment: "country of origin table");

            migrationBuilder.CreateTable(
                name: "FilmingLocations",
                columns: table => new
                {
                    FilmingLocationId = table.Column<int>(type: "int", nullable: false, comment: "filming location id")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "filming location name")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmingLocations", x => x.FilmingLocationId);
                },
                comment: "filming location table");

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false, comment: "genre id")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "genre name")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreId);
                },
                comment: "genre table");

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    LanguageId = table.Column<int>(type: "int", nullable: false, comment: "language id")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "language name")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.LanguageId);
                },
                comment: "language table");

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    PhotoId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "photo id"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "photo url")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.PhotoId);
                },
                comment: "photo table");

            migrationBuilder.CreateTable(
                name: "RegionsOfResidence",
                columns: table => new
                {
                    RegionOfResidenceId = table.Column<int>(type: "int", nullable: false, comment: "region of residence id")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "region of residence name")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionsOfResidence", x => x.RegionOfResidenceId);
                },
                comment: "region of residence table");

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

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: true, comment: "user gender"),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "user date of birth"),
                    RegionOfResidenceId = table.Column<int>(type: "int", nullable: true, comment: "user region of residence id"),
                    Bio = table.Column<string>(type: "nvarchar(1200)", maxLength: 1200, nullable: true, comment: "user bio"),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_RegionsOfResidence_RegionOfResidenceId",
                        column: x => x.RegionOfResidenceId,
                        principalTable: "RegionsOfResidence",
                        principalColumn: "RegionOfResidenceId");
                },
                comment: "user extension table");

            migrationBuilder.CreateTable(
                name: "Shows",
                columns: table => new
                {
                    ShowId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "show id"),
                    ShowTypeId = table.Column<int>(type: "int", nullable: false, comment: "show type id"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "show title"),
                    Duration = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "movie duration"),
                    EpisodeDuration = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "tv series episode duration"),
                    PhotoId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "show photo id"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "show description id"),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "show release date"),
                    NumberOfEpisodes = table.Column<int>(type: "int", nullable: true, comment: "tv series number of episodes"),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "tv series end date"),
                    Season = table.Column<int>(type: "int", nullable: true, comment: "tv series season"),
                    SeriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "tv series id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shows", x => x.ShowId);
                    table.ForeignKey(
                        name: "FK_Shows_Photos_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Photos",
                        principalColumn: "PhotoId");
                    table.ForeignKey(
                        name: "FK_Shows_ShowTypes_ShowTypeId",
                        column: x => x.ShowTypeId,
                        principalTable: "ShowTypes",
                        principalColumn: "ShowTypeId");
                    table.ForeignKey(
                        name: "FK_Shows_Shows_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Shows",
                        principalColumn: "ShowId");
                },
                comment: "show table");

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    ShowId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "rating show id"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "rating user id"),
                    Stars = table.Column<int>(type: "int", nullable: false, comment: "rating stars")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => new { x.UserId, x.ShowId });
                    table.ForeignKey(
                        name: "FK_Ratings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ratings_Shows_ShowId",
                        column: x => x.ShowId,
                        principalTable: "Shows",
                        principalColumn: "ShowId");
                },
                comment: "rating table");

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "review id"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "review user id"),
                    ShowId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "review show id"),
                    Heading = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "review title"),
                    Content = table.Column<string>(type: "nvarchar(1200)", maxLength: 1200, nullable: false, comment: "review content"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "review created at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_Shows_ShowId",
                        column: x => x.ShowId,
                        principalTable: "Shows",
                        principalColumn: "ShowId");
                },
                comment: "review table");

            migrationBuilder.CreateTable(
                name: "ShowsCountriesOfOrigin",
                columns: table => new
                {
                    ShowId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "show id"),
                    CountryOfOriginid = table.Column<int>(type: "int", nullable: false, comment: "country of origin id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShowsCountriesOfOrigin", x => new { x.ShowId, x.CountryOfOriginid });
                    table.ForeignKey(
                        name: "FK_ShowsCountriesOfOrigin_CountriesOfOrigin_CountryOfOriginid",
                        column: x => x.CountryOfOriginid,
                        principalTable: "CountriesOfOrigin",
                        principalColumn: "CountryOfOriginId");
                    table.ForeignKey(
                        name: "FK_ShowsCountriesOfOrigin_Shows_ShowId",
                        column: x => x.ShowId,
                        principalTable: "Shows",
                        principalColumn: "ShowId");
                },
                comment: "show country of origin table");

            migrationBuilder.CreateTable(
                name: "ShowsFilmingLocations",
                columns: table => new
                {
                    ShowId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "show id"),
                    FilmingLocationId = table.Column<int>(type: "int", nullable: false, comment: "filming location id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShowsFilmingLocations", x => new { x.ShowId, x.FilmingLocationId });
                    table.ForeignKey(
                        name: "FK_ShowsFilmingLocations_FilmingLocations_FilmingLocationId",
                        column: x => x.FilmingLocationId,
                        principalTable: "FilmingLocations",
                        principalColumn: "FilmingLocationId");
                    table.ForeignKey(
                        name: "FK_ShowsFilmingLocations_Shows_ShowId",
                        column: x => x.ShowId,
                        principalTable: "Shows",
                        principalColumn: "ShowId");
                },
                comment: "show filming location table");

            migrationBuilder.CreateTable(
                name: "ShowsGenres",
                columns: table => new
                {
                    ShowId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "show id"),
                    GenreId = table.Column<int>(type: "int", nullable: false, comment: "genre id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShowsGenres", x => new { x.ShowId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_ShowsGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId");
                    table.ForeignKey(
                        name: "FK_ShowsGenres_Shows_ShowId",
                        column: x => x.ShowId,
                        principalTable: "Shows",
                        principalColumn: "ShowId");
                },
                comment: "show genre table");

            migrationBuilder.CreateTable(
                name: "ShowsLanguages",
                columns: table => new
                {
                    ShowId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "show id"),
                    LanguageId = table.Column<int>(type: "int", nullable: false, comment: "langauge id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShowsLanguages", x => new { x.ShowId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_ShowsLanguages_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "LanguageId");
                    table.ForeignKey(
                        name: "FK_ShowsLanguages_Shows_ShowId",
                        column: x => x.ShowId,
                        principalTable: "Shows",
                        principalColumn: "ShowId");
                },
                comment: "show language table");

            migrationBuilder.CreateTable(
                name: "WachListItems",
                columns: table => new
                {
                    ShowId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "watch list item show id"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "watch list item user id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WachListItems", x => new { x.UserId, x.ShowId });
                    table.ForeignKey(
                        name: "FK_WachListItems_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WachListItems_Shows_ShowId",
                        column: x => x.ShowId,
                        principalTable: "Shows",
                        principalColumn: "ShowId");
                },
                comment: "watch list item table");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RegionOfResidenceId",
                table: "AspNetUsers",
                column: "RegionOfResidenceId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ShowId",
                table: "Ratings",
                column: "ShowId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ShowId",
                table: "Reviews",
                column: "ShowId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Shows_PhotoId",
                table: "Shows",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Shows_SeriesId",
                table: "Shows",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Shows_ShowTypeId",
                table: "Shows",
                column: "ShowTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ShowsCountriesOfOrigin_CountryOfOriginid",
                table: "ShowsCountriesOfOrigin",
                column: "CountryOfOriginid");

            migrationBuilder.CreateIndex(
                name: "IX_ShowsFilmingLocations_FilmingLocationId",
                table: "ShowsFilmingLocations",
                column: "FilmingLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ShowsGenres_GenreId",
                table: "ShowsGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_ShowsLanguages_LanguageId",
                table: "ShowsLanguages",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_WachListItems_ShowId",
                table: "WachListItems",
                column: "ShowId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "ShowsCountriesOfOrigin");

            migrationBuilder.DropTable(
                name: "ShowsFilmingLocations");

            migrationBuilder.DropTable(
                name: "ShowsGenres");

            migrationBuilder.DropTable(
                name: "ShowsLanguages");

            migrationBuilder.DropTable(
                name: "WachListItems");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "CountriesOfOrigin");

            migrationBuilder.DropTable(
                name: "FilmingLocations");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Shows");

            migrationBuilder.DropTable(
                name: "RegionsOfResidence");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "ShowTypes");
        }
    }
}
