﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.CountryOfOrigin", b =>
                {
                    b.Property<int>("CountryOfOriginId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("country of origin id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CountryOfOriginId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("country of origin name");

                    b.HasKey("CountryOfOriginId");

                    b.ToTable("CountriesOfOrigin", t =>
                        {
                            t.HasComment("country of origin table");
                        });
                });

            modelBuilder.Entity("Domain.FilmingLocation", b =>
                {
                    b.Property<int>("FilmingLocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("filming location id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FilmingLocationId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("filming location name");

                    b.HasKey("FilmingLocationId");

                    b.ToTable("FilmingLocations", t =>
                        {
                            t.HasComment("filming location table");
                        });
                });

            modelBuilder.Entity("Domain.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("genre id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GenreId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("genre name");

                    b.HasKey("GenreId");

                    b.ToTable("Genres", t =>
                        {
                            t.HasComment("genre table");
                        });
                });

            modelBuilder.Entity("Domain.Language", b =>
                {
                    b.Property<int>("LanguageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("language id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LanguageId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasComment("language name");

                    b.HasKey("LanguageId");

                    b.ToTable("Languages", t =>
                        {
                            t.HasComment("language table");
                        });
                });

            modelBuilder.Entity("Domain.Photo", b =>
                {
                    b.Property<string>("PhotoId")
                        .HasColumnType("nvarchar(450)")
                        .HasComment("photo id");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasComment("photo url");

                    b.HasKey("PhotoId");

                    b.ToTable("Photos", t =>
                        {
                            t.HasComment("photo table");
                        });
                });

            modelBuilder.Entity("Domain.Rating", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("rating user id");

                    b.Property<Guid>("ShowId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("rating show id");

                    b.Property<int>("Stars")
                        .HasColumnType("int")
                        .HasComment("rating stars");

                    b.HasKey("UserId", "ShowId");

                    b.HasIndex("ShowId");

                    b.ToTable("Ratings", null, t =>
                        {
                            t.HasComment("rating table");
                        });
                });

            modelBuilder.Entity("Domain.RegionOfResidence", b =>
                {
                    b.Property<int>("RegionOfResidenceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("region of residence id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RegionOfResidenceId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasComment("region of residence name");

                    b.HasKey("RegionOfResidenceId");

                    b.ToTable("RegionsOfResidence", t =>
                        {
                            t.HasComment("region of residence table");
                        });
                });

            modelBuilder.Entity("Domain.Review", b =>
                {
                    b.Property<Guid>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("review id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1200)
                        .HasColumnType("nvarchar(1200)")
                        .HasComment("review content");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasComment("review created at");

                    b.Property<string>("Heading")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasComment("review title");

                    b.Property<Guid>("ShowId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("review show id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("review user id");

                    b.HasKey("ReviewId");

                    b.HasIndex("ShowId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews", t =>
                        {
                            t.HasComment("review table");
                        });
                });

            modelBuilder.Entity("Domain.Show", b =>
                {
                    b.Property<Guid>("ShowId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("show id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasComment("show description id");

                    b.Property<DateTime?>("Duration")
                        .HasColumnType("datetime2")
                        .HasComment("movie duration");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2")
                        .HasComment("tv series end date");

                    b.Property<DateTime?>("EpisodeDuration")
                        .HasColumnType("datetime2")
                        .HasComment("tv series episode duration");

                    b.Property<int?>("NumberOfEpisodes")
                        .HasColumnType("int")
                        .HasComment("tv series number of episodes");

                    b.Property<string>("PhotoId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasComment("show photo id");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2")
                        .HasComment("show release date");

                    b.Property<int?>("Season")
                        .HasColumnType("int")
                        .HasComment("tv series season");

                    b.Property<Guid?>("SeriesId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("tv series id");

                    b.Property<int>("ShowType")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasComment("show title");

                    b.HasKey("ShowId");

                    b.HasIndex("PhotoId");

                    b.HasIndex("SeriesId");

                    b.ToTable("Shows", t =>
                        {
                            t.HasComment("show table");
                        });
                });

            modelBuilder.Entity("Domain.ShowCountryOfOrigin", b =>
                {
                    b.Property<Guid>("ShowId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("show id");

                    b.Property<int>("CountryOfOriginid")
                        .HasColumnType("int")
                        .HasComment("country of origin id");

                    b.HasKey("ShowId", "CountryOfOriginid");

                    b.HasIndex("CountryOfOriginid");

                    b.ToTable("ShowsCountriesOfOrigin", null, t =>
                        {
                            t.HasComment("show country of origin table");
                        });
                });

            modelBuilder.Entity("Domain.ShowFilmingLocation", b =>
                {
                    b.Property<Guid>("ShowId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("show id");

                    b.Property<int>("FilmingLocationId")
                        .HasColumnType("int")
                        .HasComment("filming location id");

                    b.HasKey("ShowId", "FilmingLocationId");

                    b.HasIndex("FilmingLocationId");

                    b.ToTable("ShowsFilmingLocations", null, t =>
                        {
                            t.HasComment("show filming location table");
                        });
                });

            modelBuilder.Entity("Domain.ShowGenre", b =>
                {
                    b.Property<Guid>("ShowId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("show id");

                    b.Property<int>("GenreId")
                        .HasColumnType("int")
                        .HasComment("genre id");

                    b.HasKey("ShowId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("ShowsGenres", null, t =>
                        {
                            t.HasComment("show genre table");
                        });
                });

            modelBuilder.Entity("Domain.ShowLanguage", b =>
                {
                    b.Property<Guid>("ShowId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("show id");

                    b.Property<int>("LanguageId")
                        .HasColumnType("int")
                        .HasComment("langauge id");

                    b.HasKey("ShowId", "LanguageId");

                    b.HasIndex("LanguageId");

                    b.ToTable("ShowsLanguages", null, t =>
                        {
                            t.HasComment("show language table");
                        });
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Bio")
                        .HasMaxLength(1200)
                        .HasColumnType("nvarchar(1200)")
                        .HasComment("user bio");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2")
                        .HasComment("user date of birth");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<int?>("Gender")
                        .HasColumnType("int")
                        .HasComment("user gender");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<int?>("RegionOfResidenceId")
                        .HasColumnType("int")
                        .HasComment("user region of residence id");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("RegionOfResidenceId");

                    b.ToTable("AspNetUsers", null, t =>
                        {
                            t.HasComment("user extension table");
                        });
                });

            modelBuilder.Entity("Domain.WatchListItem", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("watch list item user id");

                    b.Property<Guid>("ShowId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("watch list item show id");

                    b.HasKey("UserId", "ShowId");

                    b.HasIndex("ShowId");

                    b.ToTable("WachListItems", null, t =>
                        {
                            t.HasComment("watch list item table");
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Domain.Rating", b =>
                {
                    b.HasOne("Domain.Show", "Show")
                        .WithMany("UserRatings")
                        .HasForeignKey("ShowId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.User", "User")
                        .WithMany("ShowRatings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Show");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Review", b =>
                {
                    b.HasOne("Domain.Show", "Show")
                        .WithMany("UserReviews")
                        .HasForeignKey("ShowId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.User", "User")
                        .WithMany("ShowReviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Show");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Show", b =>
                {
                    b.HasOne("Domain.Photo", "Photo")
                        .WithMany("Shows")
                        .HasForeignKey("PhotoId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Show", "Series")
                        .WithMany("Episodes")
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Photo");

                    b.Navigation("Series");
                });

            modelBuilder.Entity("Domain.ShowCountryOfOrigin", b =>
                {
                    b.HasOne("Domain.CountryOfOrigin", "CountryOfOrigin")
                        .WithMany("Shows")
                        .HasForeignKey("CountryOfOriginid")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Show", "Show")
                        .WithMany("CountriesOfOrigin")
                        .HasForeignKey("ShowId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("CountryOfOrigin");

                    b.Navigation("Show");
                });

            modelBuilder.Entity("Domain.ShowFilmingLocation", b =>
                {
                    b.HasOne("Domain.FilmingLocation", "FilmingLocation")
                        .WithMany("Shows")
                        .HasForeignKey("FilmingLocationId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Show", "Show")
                        .WithMany("FilmingLocations")
                        .HasForeignKey("ShowId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("FilmingLocation");

                    b.Navigation("Show");
                });

            modelBuilder.Entity("Domain.ShowGenre", b =>
                {
                    b.HasOne("Domain.Genre", "Genre")
                        .WithMany("Shows")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Show", "Show")
                        .WithMany("Genres")
                        .HasForeignKey("ShowId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("Show");
                });

            modelBuilder.Entity("Domain.ShowLanguage", b =>
                {
                    b.HasOne("Domain.Language", "Language")
                        .WithMany("Shows")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Show", "Show")
                        .WithMany("Languages")
                        .HasForeignKey("ShowId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Language");

                    b.Navigation("Show");
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.HasOne("Domain.RegionOfResidence", "RegionOfResidence")
                        .WithMany()
                        .HasForeignKey("RegionOfResidenceId");

                    b.Navigation("RegionOfResidence");
                });

            modelBuilder.Entity("Domain.WatchListItem", b =>
                {
                    b.HasOne("Domain.Show", "Show")
                        .WithMany("WatchListItems")
                        .HasForeignKey("ShowId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.User", "User")
                        .WithMany("WatchList")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Show");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.CountryOfOrigin", b =>
                {
                    b.Navigation("Shows");
                });

            modelBuilder.Entity("Domain.FilmingLocation", b =>
                {
                    b.Navigation("Shows");
                });

            modelBuilder.Entity("Domain.Genre", b =>
                {
                    b.Navigation("Shows");
                });

            modelBuilder.Entity("Domain.Language", b =>
                {
                    b.Navigation("Shows");
                });

            modelBuilder.Entity("Domain.Photo", b =>
                {
                    b.Navigation("Shows");
                });

            modelBuilder.Entity("Domain.Show", b =>
                {
                    b.Navigation("CountriesOfOrigin");

                    b.Navigation("Episodes");

                    b.Navigation("FilmingLocations");

                    b.Navigation("Genres");

                    b.Navigation("Languages");

                    b.Navigation("UserRatings");

                    b.Navigation("UserReviews");

                    b.Navigation("WatchListItems");
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.Navigation("ShowRatings");

                    b.Navigation("ShowReviews");

                    b.Navigation("WatchList");
                });
#pragma warning restore 612, 618
        }
    }
}
