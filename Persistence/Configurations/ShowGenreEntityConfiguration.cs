namespace Persistence.Configurations
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ShowGenreEntityConfiguration : IEntityTypeConfiguration<ShowGenre>
    {
        public void Configure(EntityTypeBuilder<ShowGenre> builder)
        {
            builder.ToTable("ShowsGenres");

            builder.HasKey(sg => new { sg.ShowId, sg.GenreId });

            builder
                .HasOne(sg => sg.Show)
                .WithMany(s => s.Genres)
                .HasForeignKey(sg => sg.ShowId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(sg => sg.Genre)
                .WithMany(g => g.Shows)
                .HasForeignKey(sg => sg.GenreId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
