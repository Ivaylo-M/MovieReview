namespace Domain
{
    using System.ComponentModel.DataAnnotations;
    
    using Microsoft.EntityFrameworkCore;
    
    using static Common.EntityValidationConstants.Genre;

    [Comment("genre table")]
    public class Genre
    {
        public Genre()
        {
            this.Shows = new HashSet<ShowGenre>();
        }

        [Comment("genre id")]
        [Key]
        public int GenreId { get; set; }

        [Comment("genre name")]
        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public ICollection<ShowGenre> Shows { get; set; } = null!;
    }
}
