namespace Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    using Microsoft.EntityFrameworkCore;

    [Comment("show language table")]
    public class ShowLanguage
    {
        [Comment("show id")]
        [Required]
        public Guid ShowId { get; set; }

        [Comment("show")]
        [ForeignKey(nameof(ShowId))]
        public Show Show { get; set; } = null!;

        [Comment("language id")]
        [Required]
        public int LanguageId { get; set; }

        [Comment("language")]
        [ForeignKey(nameof(LanguageId))]
        public Language Language { get; set; } = null!;
    }
}