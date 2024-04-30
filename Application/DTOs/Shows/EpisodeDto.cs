namespace Application.DTOs.Shows
{
    public class EpisodeDto
    {
        public string ShowId { get; set; } = null!;

        public string PhotoUrl { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int Season { get; set; } 

        public int EpisodeNumber { get; set; }

        public DateTime ReleaseDate { get; set; }

        public float AverageRating { get; set; }

        public int NumberOfRatings { get; set; }

        public int MyRating { get; set; }
    }
}
