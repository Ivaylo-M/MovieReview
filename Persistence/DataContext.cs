namespace Persistence
{
    using System.Reflection;
    
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    
    using Domain;

    public class DataContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DataContext(DbContextOptions options)
            :base(options)
        {  
        }

        public DbSet<Review> Reviews { get; set; } = null!;

        public DbSet<ShowType> ShowTypes { get; set; } = null!;

        public DbSet<Show> Shows { get; set; } = null!;

        public DbSet<Photo> Photos { get; set; } = null!;

        public DbSet<Genre> Genres { get; set; } = null!;

        public DbSet<CountryOfOrigin> CountriesOfOrigin { get; set; } = null!;

        public DbSet<Language> Languages { get; set; } = null!;

        public DbSet<FilmingLocation> FilmingLocations { get; set; }

        public DbSet<RegionOfResidence> RegionsOfResidence { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            Assembly assembly = Assembly.GetAssembly(typeof(DataContext)) ??
                                Assembly.GetExecutingAssembly();

            builder.ApplyConfigurationsFromAssembly(assembly);

            base.OnModelCreating(builder);
        }
    }
}
