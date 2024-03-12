namespace Domain
{
    using System.ComponentModel.DataAnnotations;
    
    using Microsoft.EntityFrameworkCore;
    
    using static Common.EntityValidationConstants.FilmingLocation;

    [Comment("filming location table")]
    public class FilmingLocation
    {
        [Comment("filming location id")]
        [Key]
        public int FilmingLocationId { get; set; }

        [Comment("filming location name")]
        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;
    }
}
