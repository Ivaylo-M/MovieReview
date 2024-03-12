namespace Domain
{
    using System.ComponentModel.DataAnnotations;
    
    using Microsoft.EntityFrameworkCore;
    
    using static Common.EntityValidationConstants.CountryOfOrigin;

    [Comment("country of origin table")]
    public class CountryOfOrigin
    {
        [Comment("country of origin id")]
        [Key]
        public int CountryOfOriginId { get; set; }

        [Comment("country of origin name")]
        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;
    }
}