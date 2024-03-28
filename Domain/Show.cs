namespace Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Domain.Enums;
    using Microsoft.EntityFrameworkCore;
    
    using static Common.EntityValidationConstants.Show;

    [Comment("show table")]
    public class Show
    {
        public Show()
        {
            this.ShowId = Guid.NewGuid();
            this.UserReviews = new HashSet<Review>();
            this.WatchListItems = new HashSet<WatchListItem>();
            this.UserRatings = new HashSet<Rating>();
            this.Genres = new HashSet<ShowGenre>();
            this.CountriesOfOrigin = new HashSet<ShowCountryOfOrigin>();
            this.Languages = new HashSet<ShowLanguage>();
            this.FilmingLocations = new HashSet<ShowFilmingLocation>();
        }

        [Comment("show id")]
        [Key]
        public Guid ShowId { get; set; }

        public ShowType ShowType { get; set; }

        [Comment("show title")]
        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Comment("movie duration")]
        public int? Duration { get; set; }

        [Comment("tv series episode duration")]
        public int? EpisodeDuration { get; set; }

        [Comment("show photo id")]
        public string? PhotoId { get; set; }

        [Comment("show photo")]
        [ForeignKey(nameof(PhotoId))]
        public Photo? Photo { get; set; }

        [Comment("show description id")]
        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Comment("show release date")]
        [Required]
        public DateTime ReleaseDate { get; set; }

        [Comment("tv series number of episodes")]
        public int? NumberOfEpisodes { get; set; }

        [Comment("tv series end date")]
        public DateTime? EndDate { get; set; }

        [Comment("tv series season")]
        public int? Season { get; set; }

        [Comment("tv series id")]
        public Guid? SeriesId { get; set; }

        [Comment("tv series")]
        [ForeignKey(nameof(SeriesId))]
        public Show? Series { get; set; }

        public ICollection<Show>? Episodes { get; set; }

        public ICollection<Rating> UserRatings { get; set; } = null!;

        public ICollection<WatchListItem> WatchListItems { get; set; } = null!;

        public ICollection<Review> UserReviews { get; set; } = null!;

        public ICollection<ShowGenre> Genres { get; set; } = null!;

        public ICollection<ShowCountryOfOrigin> CountriesOfOrigin { get; set; } = null!;

        public ICollection<ShowLanguage> Languages { get; set; } = null!;

        public ICollection<ShowFilmingLocation> FilmingLocations { get; set; } = null!;
    }
}
