namespace Domain
{
    using System.ComponentModel.DataAnnotations;
    
    using Microsoft.EntityFrameworkCore;

    [Comment("photo table")]
    public class Photo
    {
        public Photo()
        {
            this.Shows = new HashSet<Show>();
        }

        [Comment("photo id")]
        [Key]
        public string PhotoId { get; set; } = null!;

        [Comment("photo url")]
        public string Url { get; set; } = null!;

        public ICollection<Show> Shows { get; set; } = null!;
    }
}