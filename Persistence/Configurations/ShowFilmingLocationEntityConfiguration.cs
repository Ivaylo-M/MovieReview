namespace Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    
    using Domain;

    public class ShowFilmingLocationEntityConfiguration : IEntityTypeConfiguration<ShowFilmingLocation>
    {
        public void Configure(EntityTypeBuilder<ShowFilmingLocation> builder)
        {
            builder.ToTable("ShowsFilmingLocations");

            builder.HasKey(sfl => new { sfl.ShowId, sfl.FilmingLocationId });

            builder
                .HasOne(sfl => sfl.Show)
                .WithMany(s => s.FilmingLocations)
                .HasForeignKey(sfl => sfl.ShowId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(sfl => sfl.FilmingLocation)
                .WithMany(fl => fl.Shows)
                .HasForeignKey(sfl => sfl.FilmingLocationId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
