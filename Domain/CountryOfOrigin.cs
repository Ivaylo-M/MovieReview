namespace Domain
{
    using System.ComponentModel.DataAnnotations;
    
    using Microsoft.EntityFrameworkCore;
    
    using static Common.EntityValidationConstants.CountryOfOrigin;

    [Comment("country of origin table")]
    public class CountryOfOrigin
    {
        public CountryOfOrigin()
        {
            this.Shows = new HashSet<ShowCountryOfOrigin>();
        }

        [Comment("country of origin id")]
        [Key]
        public int CountryOfOriginId { get; set; }

        [Comment("country of origin name")]
        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public ICollection<ShowCountryOfOrigin> Shows { get; set; } = null!;
    }
}