namespace Domain
{
    using System.ComponentModel.DataAnnotations;
    
    using Microsoft.EntityFrameworkCore;
    
    using static Common.EntityValidationConstants.Genre;

    [Comment("genre table")]
    public class Genre
    {
        [Comment("genre id")]
        [Key]
        public int GenreId { get; set; }

        [Comment("genre name")]
        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;
    }
}
