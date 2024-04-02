namespace Application.DTOs.Shows
{
    public class ShowEditShowDto : ShowAddShowDto
    {
        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime ReleaseDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? Season { get; set; }

        public int? Duration { get; set; }
    }
}
