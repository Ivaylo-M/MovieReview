namespace Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    
    using Domain;

    public class WatchListItemEntityConfiguration : IEntityTypeConfiguration<WatchListItem>
    {
        public void Configure(EntityTypeBuilder<WatchListItem> builder)
        {
            builder.ToTable("WachListItems");

            builder.HasKey(wli => new { wli.UserId, wli.ShowId });

            builder
                .HasOne(wli => wli.User)
                .WithMany(u => u.WatchList)
                .HasForeignKey(wli => wli.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(wli => wli.Show)
                .WithMany(s => s.WatchListItems)
                .HasForeignKey(wli => wli.ShowId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}