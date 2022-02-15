using Microsoft.EntityFrameworkCore;
using TL.Domain;
using TL.Domain.ValueObjects.ArtistValueObjects;
using TL.Domain.ValueObjects.TrackValueObjects;

namespace TL.Data;

public class TuneLibraryContext : DbContext
{
    public DbSet<Tune> Tunes { get; set; }
    public DbSet<Album> Albums { get; set; }
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Track> Tracks { get; set; }
    public DbSet<TrackTune> TrackTunes { get; set; }

    public TuneLibraryContext(){ } //have to specify a default constructor

    public TuneLibraryContext(DbContextOptions opt) : base(opt){ } //in the tests, we can explicitly 
    //create the context and tell it to use InMemory

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        //gives flexibility to use the SQL server provider *if needed*. If the options
        //are not passed into the constructor of the context, OnConfiguring will use the SQL server
        //connection. If options are specified, such as InMemory, OnConfiguring will skip over
        //the following code:
        if (!options.IsConfigured)
        {
            options.UseNpgsql("Host=localhost;Username=postgres;Password=postgres;Database=TuneLibrary");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tune>()
            .Property(t => t.TuneType)
            .HasConversion(t => t.ToString(),
                t => (TuneTypeEnum) Enum.Parse(typeof(TuneTypeEnum), t));

        modelBuilder.Entity<Tune>()
            .Property(t => t.TuneKey)
            .HasConversion(t => t.ToString(),
                t => (TuneKeyEnum) Enum.Parse(typeof(TuneKeyEnum), t));

        modelBuilder.Entity<Tune>()
            .Property(t => t.DateAdded)
            .HasConversion(
                d => d.ToString("MM/dd/yyyy"),
                d => DateOnly.Parse(d));

        modelBuilder.Entity<Tune>()
            .OwnsMany(x => x.AlternateTitles)
            .Property(a => a.Value)
            .HasColumnName("Title");
        
        // modelBuilder.Entity<Tune>()
        //     .HasMany(t => t.FeaturedOnTrack)
        //     .WithOne(t => t.);

        modelBuilder.Entity<Track>()
            .HasMany(t => t.TrackTunes)
            .WithOne(t => t.Track)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Album>()
            .HasMany(a => a.Artists)
            .WithMany(a => a.Albums);

        modelBuilder.Entity<Track>()
            .HasOne(t => t.Album)
            .WithMany(a => a.TrackListing)
            .HasForeignKey(t => t.AlbumId)
            .IsRequired();

        modelBuilder.Entity<Album>()
            .Navigation(a => a.TrackListing)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

        modelBuilder.Entity<TrackTune>()
            .Navigation(t => t.Track)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

        modelBuilder.Entity<TrackTune>()
            .Navigation(t => t.Tune)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

        modelBuilder.Entity<Artist>()
            .OwnsOne(a => a.Name)
            .Property(x => x.Value)
            .HasColumnName("Name");

        modelBuilder.Entity<Album>()
            .OwnsOne(a => a.Title)
            .Property(x => x.Value)
            .HasColumnName("Title");

        modelBuilder.Entity<Album>()
            .OwnsOne(a => a.Year)
            .Property(x => x.Value)
            .HasColumnName("Year");

        modelBuilder.Entity<Track>()
            .OwnsOne(t => t.Title)
            .Property(x => x.Value)
            .HasColumnName("Title");

        modelBuilder.Entity<Track>()
            .OwnsOne(t => t.TrackNumber)
            .Property(x => x.Value)
            .HasColumnName("TrackNumber");

        modelBuilder.Entity<Tune>()
            .OwnsOne(t => t.Title)
            .Property(x => x.Value)
            .HasColumnName("Title");
        
        modelBuilder.Entity<Tune>()
            .OwnsOne(t => t.Composer)
            .Property(x => x.Value)
            .HasColumnName("Composer");

        modelBuilder.Entity<TrackTune>()
            .OwnsOne(t => t.Order)
            .Property(x => x.Value)
            .HasColumnName("Order");

    }
}