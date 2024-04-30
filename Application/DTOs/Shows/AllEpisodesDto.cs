namespace Application.DTOs.Shows
{
    public class AllEpisodesDto
    {
        public TVSeriesDto Series { get; set; } = null!;

        public IEnumerable<EpisodeDto> Episodes { get; set; } = null!;
    }
}
