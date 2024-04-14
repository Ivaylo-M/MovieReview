namespace Application.DTOs.Shows
{
    using Domain.Enums;

    public class AllShowsDto
    {
        public string ShowId { get; set; } = null!;

        public string? PhotoUrl { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int ReleaseYear { get; set; }

        public int? EndYear { get; set; }

        public int? Duration { get; set; }

        public float AverageRating { get; set; }

        public int NumberOfRatings { get; set; }

        public int? MyRating { get; set; }

        public ShowType ShowType { get; set; }

        public IEnumerable<int> Genres { get; set; } = null!;
    }
}
