namespace Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    
    using Domain;

    public class ShowLanguageEntityConfiguration : IEntityTypeConfiguration<ShowLanguage>
    {
        public void Configure(EntityTypeBuilder<ShowLanguage> builder)
        {
            builder.ToTable("ShowsLanguages");

            builder.HasKey(sl => new { sl.ShowId, sl.LanguageId });

            builder
                .HasOne(sl => sl.Show)
                .WithMany(s => s.Languages)
                .HasForeignKey(sl => sl.ShowId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(sl => sl.Language)
                .WithMany(l => l.Shows)
                .HasForeignKey(sl => sl.LanguageId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
