namespace Application.DTOs.Shows
{
    public class AllShowsDto
    {
        public string ShowId { get; set; } = null!;

        public string ShowType { get; set; } = null!;

        public string PhotoUrl { get; set; } = null!;

        public string Title { get; set; } = null!;

        public int ReleaseYear { get; set; }

        public int? EndYear { get; set; }

        public float AverageRating { get; set; }

        public int MyRating { get; set; }

        public string Description { get; set; } = null!;
    }
}
