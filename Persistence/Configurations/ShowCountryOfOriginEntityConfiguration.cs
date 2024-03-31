namespace Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    
    using Domain;

    public class ShowCountryOfOriginEntityConfiguration : IEntityTypeConfiguration<ShowCountryOfOrigin>
    {
        public void Configure(EntityTypeBuilder<ShowCountryOfOrigin> builder)
        {
            builder.ToTable("ShowsCountriesOfOrigin");

            builder.HasKey(scoo => new { scoo.ShowId, scoo.CountryOfOriginId });

            builder
                .HasOne(scoo => scoo.Show)
                .WithMany(s => s.CountriesOfOrigin)
                .HasForeignKey(scoo => scoo.ShowId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(scoo => scoo.CountryOfOrigin)
                .WithMany(coo => coo.Shows)
                .HasForeignKey(scoo => scoo.CountryOfOriginId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
