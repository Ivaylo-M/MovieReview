namespace Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    using Microsoft.EntityFrameworkCore;

    [Comment("show country of origin table")]
    public class ShowCountryOfOrigin
    {
        [Comment("show id")]
        [Required]
        public Guid ShowId { get; set; }

        [Comment("show")]
        [ForeignKey(nameof(ShowId))]
        public Show Show { get; set; } = null!;

        [Comment("country of origin id")]
        [Required]
        public int CountryOfOriginId { get; set; }

        [Comment("country of origin")]
        [ForeignKey(nameof(CountryOfOriginId))]
        public CountryOfOrigin CountryOfOrigin { get; set; } = null!;
    }
}