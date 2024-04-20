namespace Application.DTOs.Reviews 
{
    public class LastReviewDto 
    {
        public string ReviewId { get; set; } = null!;

        public string Heading { get; set; } = null!;

        public string Content { get; set; } = null!;

        public DateTime CreatedAt { get; set;}

        public bool IsMine { get; set; }
    }
}