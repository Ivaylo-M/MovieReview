namespace Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    using Microsoft.EntityFrameworkCore;

    [Comment("rating table")]
    public class Rating
    {
        [Comment("rating show id")]
        [Required]
        public Guid ShowId { get; set; }

        [Comment("rating show")]
        [ForeignKey(nameof(ShowId))]
        public Show Show { get; set; } = null!;

        [Comment("rating user id")]
        [Required]
        public Guid UserId { get; set; }

        [Comment("rating user")]
        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        [Comment("rating stars")]
        [Required]
        public int Stars { get; set; }
    }
}