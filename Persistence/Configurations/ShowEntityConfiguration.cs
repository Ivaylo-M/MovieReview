namespace Persistence.Configurations
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ShowEntityConfiguration : IEntityTypeConfiguration<Show>
    {
        public void Configure(EntityTypeBuilder<Show> builder)
        {
            builder
                .HasMany(s => s.Episodes)
                .WithOne(s => s.Series)
                .HasForeignKey(s => s.SeriesId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(s => s.Photo)
                .WithMany(p => p.Shows)
                .HasForeignKey(s => s.PhotoId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(s => s.ShowType)
                .WithMany(st => st.Shows)
                .HasForeignKey(s => s.ShowTypeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
