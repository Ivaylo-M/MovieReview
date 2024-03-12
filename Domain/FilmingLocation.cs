namespace Domain
{
    using System.ComponentModel.DataAnnotations;
    
    using Microsoft.EntityFrameworkCore;
    
    using static Common.EntityValidationConstants.FilmingLocation;

    [Comment("filming location table")]
    public class FilmingLocation
    {
        public FilmingLocation()
        {
            this.Shows = new HashSet<ShowFilmingLocation>();
        }

        [Comment("filming location id")]
        [Key]
        public int FilmingLocationId { get; set; }

        [Comment("filming location name")]
        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public ICollection<ShowFilmingLocation> Shows { get; set; } = null!;
    }
}
