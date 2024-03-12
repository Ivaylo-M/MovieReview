namespace Domain
{
    using System.ComponentModel.DataAnnotations;
    
    using Microsoft.EntityFrameworkCore;
    
    using static Common.EntityValidationConstants.ShowType;

    [Comment("show type table")]
    public class ShowType
    {
        [Comment("show type id")]
        [Key]
        public int ShowTypeId { get; set; }

        [Comment("show type name")]
        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public ICollection<Show> Shows { get; set; } = null!;
    }
}