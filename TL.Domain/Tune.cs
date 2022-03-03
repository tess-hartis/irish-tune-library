using TL.Domain.ValueObjects.TuneValueObjects;
using LanguageExt;

namespace TL.Domain;

public class Tune
{
    private Tune() { }

    public int Id { get; }
    public TuneTitle Title { get; private set; }

    private readonly List<TuneTitle> _alternateTitles = new List<TuneTitle>();
    public IReadOnlyList<TuneTitle> AlternateTitles =>
        _alternateTitles;

    public TuneTypeValueObj TuneType { get; private set; }
    public TuneKeyValueObj TuneKey { get; private set; }
    public TuneComposer Composer { get; private set; }
    public DateOnly DateAdded { get; private init; }
    
    private readonly List<TrackTune> _featuredOnTrack = new List<TrackTune>();
    public IEnumerable<Track> FeaturedOnTrack => 
        _featuredOnTrack.Select(x => x.Track).ToList();

    public static Tune Create(TuneTitle title, TuneComposer composer, TuneTypeValueObj type, TuneKeyValueObj key)
    {
        var tune = new Tune
        {
            Title = title,
            Composer = composer,
            TuneType = type,
            TuneKey = key,
            DateAdded = DateOnly.FromDateTime(DateTime.Today)
        };
        
        return tune;
    }

    public Unit AddAlternateTitle(TuneTitle title)
    {
        _alternateTitles.Add(title);
        return Unit.Default;
    }

    public bool RemoveAlternateTitle(TuneTitle title)
    {
        return _alternateTitles.Remove(title);
    }

    public Unit Update(TuneTitle title, TuneComposer composer, TuneTypeValueObj type, TuneKeyValueObj key)
    {
        Title = title;
        Composer = composer;
        TuneType = type;
        TuneKey = key;
        
        return Unit.Default;
    }
}
