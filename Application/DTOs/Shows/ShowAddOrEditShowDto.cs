namespace Application.DTOs.Shows
{
    using Application.DTOs.CountriesOfOrigin;
    using Application.DTOs.FilmingLocations;
    using Application.DTOs.Genres;
    using Application.DTOs.Languages;
    using Application.DTOs.Photos;
    using Application.DTOs.ShowTypes;

    public class ShowAddOrEditShowDto
    {
        public TVSeriesDto? Series { get; set; } = null!;

        public ShowTypeDto ShowType { get; set; } = null!;

        public IEnumerable<FilmingLocationDto>? FilmingLocations { get; set; } = null!;

        public IEnumerable<GenreDto>? Genres { get; set; } = null!;

        public IEnumerable<LanguageDto>? Languages { get; set; } = null!;

        public IEnumerable<CountryOfOriginDto>? CountriesOfOrigin { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime ReleaseDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? Season { get; set; }

        public int? Duration { get; set; }

        public PhotoDto? Photo { get; set; } = null!;
    }
}
