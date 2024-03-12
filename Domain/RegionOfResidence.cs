namespace Domain
{
    using System.ComponentModel.DataAnnotations;
    
    using Microsoft.EntityFrameworkCore;
    
    using static Common.EntityValidationConstants.RegionOfResidence;

    [Comment("region of residence table")]
    public class RegionOfResidence
    {
        [Comment("region of residence id")]
        [Key]
        public int RegionOfResidenceId { get; set; }

        [Comment("region of residence name")]
        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;
    }
}