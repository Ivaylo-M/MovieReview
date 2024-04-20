namespace Application.DTOs.Shows 
{
    using Application.DTOs.Reviews;
    using Domain.Enums;

    public class ShowDetailsDto 
    {
        public string ShowId { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime ReleaseDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int Duration { get; set; }

        public float AverageRating { get; set; } 

        public int? MyRating { get; set; }

        public int NumberOfRatings { get; set; }

        public string? PhotoUrl { get; set; }

        public ShowType ShowType { get; set; }

        public LastReviewDto? LastReview { get; set; }

        public IEnumerable<string> Genres { get; set; } = null!;

        public IEnumerable<string> FilmingLocations { get; set; } = null!;

        public IEnumerable<string> Languages { get; set; } = null!;

        public IEnumerable<string> CountriesOfOrigin { get; set; } = null!;

        public TVSeriesDto? TVSeries { get; set; }

        public int? Season { get; set; }

        public int? EpisodeNumber { get; set; }
    }
}