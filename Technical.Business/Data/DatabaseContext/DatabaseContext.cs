using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Technical.Business.Data
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Brewery>().
                 HasMany(b => b.Beers).
                 WithMany(b => b.Breweries).
                 UsingEntity<BreweryBeerMapping>();

            modelBuilder.Entity<Bar>().
                HasMany(b => b.Beers).
                WithMany(b => b.Bars).
                UsingEntity<BeerBarMapping>();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Beer> Beers { get; set; }
        public DbSet<Bar> Bars { get; set; }
        public DbSet<Brewery> Breweries { get; set; }
        public DbSet<BreweryBeerMapping> BrewersBeersMapping { get; set; }
        public DbSet<BeerBarMapping> BeersBarsMapping { get; set; }
    }
    public class Beer
    {
        [Key]
        public Guid BeerId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal PercentageAlcoholByVolume { get; set; }
        public List<Brewery> Breweries { get; set; } = new();
        public List<Bar> Bars { get; set; } = new();

    }
    public class Brewery
    {
        [Key]
        public Guid BreweryId { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Beer> Beers { get; set; } = new();
    }
    public class Bar
    {
        [Key]
        public Guid BarId { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Beer> Beers { get; set; } = new();

    }

    public class BreweryBeerMapping
    {
        [Key]
        public Guid Id { get; set; }
        public Guid BreweryId { get; set; }
        public Guid BeerId { get; set; }

        //foreign relations
        public virtual Brewery Brewery { get; set; }
        public virtual Beer Beer { get; set; }

    }
    public class BeerBarMapping
    {
        [Key]
        public Guid Id { get; set; }
        public Guid BeerId { get; set; }
        public Guid BarId { get; set; }

        //foreign relation
        public virtual Beer Beer { get; set; }
        public virtual Bar Bar { get; set; }

    }
}
