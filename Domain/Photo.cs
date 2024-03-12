namespace Domain
{
    using System.ComponentModel.DataAnnotations;
    
    using Microsoft.EntityFrameworkCore;

    [Comment("photo table")]
    public class Photo
    {
        [Comment("photo id")]
        [Key]
        public string PhotoId { get; set; } = null!;

        [Comment("photo url")]
        public string Url { get; set; } = null!;
    }
}