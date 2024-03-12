namespace Domain
{
    using System.ComponentModel.DataAnnotations;
    
    using Microsoft.EntityFrameworkCore;
    
    using static Common.EntityValidationConstants.Language;

    [Comment("language table")]
    public class Language
    {
        public Language()
        {
            this.Shows = new HashSet<ShowLanguage>();
        }

        [Comment("language id")]
        [Key]
        public int LanguageId { get; set; }

        [Comment("language name")]
        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public ICollection<ShowLanguage> Shows { get; set; } = null!;
    }
}