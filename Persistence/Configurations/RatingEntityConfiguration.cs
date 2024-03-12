namespace Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    
    using Domain;

    public class RatingEntityConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.ToTable("Ratings");

            builder.HasKey(r => new { r.UserId, r.ShowId });

            builder
                .HasOne(r => r.User)
                .WithMany(u => u.ShowRatings)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(r => r.Show)
                .WithMany(s => s.UserRatings)
                .HasForeignKey(r => r.ShowId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
