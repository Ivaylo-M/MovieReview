namespace Application.DTOs.Shows
{
    using Domain.Enums;
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Show;

    public class AddShowDto
    {
        public ShowType ShowType { get; set; }

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        public DateTime ReleaseDate { get; set; }

        public int? Duration { get; set; }

        public DateTime? EndDate { get; set; }

        public int? Season { get; set; }

        public string? SeriesId { get; set; } = null!;

        public IEnumerable<int>? Languages { get; set; } = null!;

        public IEnumerable<int>? FilmingLocations { get; set; } = null!;

        public IEnumerable<int>? Genres { get; set; } = null!;

        public IEnumerable<int>? CountriesOfOrigin { get; set; } = null!;
    }
}
