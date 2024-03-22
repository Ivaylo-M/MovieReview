namespace Application.DTOs.Shows
{
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Show;

    public class AddShowDto
    {
        public string ShowTypeId { get; set; } = null!;

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

        public IFormFile Photo { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        public DateTime ReleaseDate { get; set; }

        public DateTime? Duration { get; set; }

        public DateTime? EpisodeDuration { get; set; }

        public int? NumberOfEpisodes { get; set; }

        public DateTime? EndDate { get; set; }

        public int? Season { get; set; }

        public string SeriesId { get; set; } = null!;

        public IEnumerable<string> Languages { get; set; } = null!;

        public IEnumerable<string> FilmingLocations { get; set; } = null!;

        public IEnumerable<string> Genres { get; set; } = null!;

        public IEnumerable<string> CountriesOfOrigin { get; set; } = null!;
    }
}
