namespace Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    using Microsoft.EntityFrameworkCore;

    [Comment("show genre table")]
    public class ShowGenre
    {
        [Comment("show id")]
        [Required]
        public Guid ShowId { get; set; }

        [Comment("show")]
        [ForeignKey(nameof(ShowId))]
        public Show Show { get; set; } = null!;

        [Comment("genre id")]
        [Required]
        public int GenreId { get; set; }

        [Comment("genre")]
        [ForeignKey(nameof(GenreId))]
        public Genre Genre { get; set; } = null!;
    }
}