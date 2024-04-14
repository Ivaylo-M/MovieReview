namespace Application.DTOs.Shows
{
    using Domain.Enums;

    public class FilterShowsDto
    {
        public string? Title { get; set; }

        public IEnumerable<ShowType>? ShowTypes { get; set; }

        public IEnumerable<int>? Genres { get; set; }

        public int? MinReleaseYear { get; set; }

        public int? MaxReleaseYear { get; set; }
    }
}
