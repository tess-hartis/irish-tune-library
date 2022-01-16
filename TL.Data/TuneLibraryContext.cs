using Microsoft.EntityFrameworkCore;
using TL.Domain;

namespace TL.Data;

public class TuneLibraryContext : DbContext
{
    public DbSet<Tune> Tunes { get; set; }
    public DbSet<Album> Albums { get; set; }
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Track> Tracks { get; set; }

    public TuneLibraryContext(){} //have to specify a default constructor

    public TuneLibraryContext(DbContextOptions opt) : base(opt){} //in the tests, we can explicitly 
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
        
    }
}