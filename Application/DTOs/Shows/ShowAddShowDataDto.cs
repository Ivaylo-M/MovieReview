namespace Application.DTOs.Shows
{
    using Domain.Enums;

    public class ShowAddShowDataDto
    {
        public ShowType ShowType { get; set; }

        public string? TVSeriesId { get; set; } = null!;
    }
}
