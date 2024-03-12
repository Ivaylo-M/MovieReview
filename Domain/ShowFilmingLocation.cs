namespace Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    using Microsoft.EntityFrameworkCore;

    [Comment("show filming location table")]
    public class ShowFilmingLocation
    {
        [Comment("show id")]
        [Required]
        public Guid ShowId { get; set; }

        [Comment("show")]
        [ForeignKey(nameof(ShowId))]
        public Show Show { get; set; } = null!;

        [Comment("filming location id")]
        [Required]
        public int FilmingLocationId { get; set; }

        [Comment("filming location")]
        [ForeignKey(nameof(FilmingLocation))]
        public FilmingLocation FilmingLocation { get; set; } = null!;
    }
}