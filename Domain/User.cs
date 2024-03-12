namespace Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;
    
    using Domain.Enums;
    
    using static Common.EntityValidationConstants.User;

    [Comment("user extension table")]
    public class User : IdentityUser<Guid>
    {
        public User()
        {
            this.Id = Guid.NewGuid();
            this.ShowReviews = new HashSet<Review>();
            this.WatchList = new HashSet<WatchListItem>();
            this.ShowRatings = new HashSet<Rating>();
        }

        [Comment("user gender")]
        public Gender? Gender { get; set; }

        [Comment("user date of birth")]
        public DateTime? DateOfBirth { get; set; }

        [Comment("user region of residence id")]
        public int? RegionOfResidenceId { get; set; }

        [Comment("user region of residence")]
        [ForeignKey(nameof(RegionOfResidenceId))]
        public RegionOfResidence? RegionOfResidence { get; set; }

        [Comment("user bio")]
        [MaxLength(BioMaxLength)]
        public string? Bio { get; set; }

        public ICollection<Review> ShowReviews { get; set; } = null!;

        public ICollection<WatchListItem> WatchList { get; set; } = null!;

        public ICollection<Rating> ShowRatings { get; set; } = null!;
    }
}