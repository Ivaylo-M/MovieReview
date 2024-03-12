namespace Persistence.Configurations
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ReviewEntityConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder
                .HasOne(r => r.User)
                .WithMany(u => u.ShowReviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(r => r.Show)
                .WithMany(s => s.UserReviews)
                .HasForeignKey(r => r.ShowId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
