namespace Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    using Microsoft.EntityFrameworkCore;

    [Comment("watch list item table")]
    public class WatchListItem
    {
        [Comment("watch list item show id")]
        [Required]
        public Guid ShowId { get; set; }

        [Comment("watch list item show")]
        [ForeignKey(nameof(ShowId))]
        public Show Show { get; set; } = null!;

        [Comment("watch list item user id")]
        [Required]
        public Guid UserId { get; set; }

        [Comment("watch list item user")]
        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;
    }
}