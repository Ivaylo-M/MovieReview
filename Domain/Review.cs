namespace Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    using Microsoft.EntityFrameworkCore;
    
    using static Common.EntityValidationConstants.Review;

    [Comment("review table")]
    public class Review
    {
        [Comment("review id")]
        [Key]
        public Guid ReviewId { get; set; }

        [Comment("review user id")]
        [Required]
        public Guid UserId { get; set; }

        [Comment("review user")]
        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        [Comment("review show id")]
        [Required]
        public Guid ShowId { get; set; }

        [Comment("review show")]
        [ForeignKey(nameof(ShowId))]
        public Show Show { get; set; } = null!;

        [Comment("review title")]
        [Required]
        [MaxLength(HeadingMaxLength)]
        public string Heading { get; set; } = null!;

        [Comment("review content")]
        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; } = null!;

        [Comment("review created at")]
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}