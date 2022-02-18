using TL.Domain.ValueObjects.TuneValueObjects;
using LanguageExt;
using static LanguageExt.Prelude;

namespace TL.Domain;

public class Tune
{
    private Tune()
    {
    }

    public int Id { get; private set; }
    public TuneTitle Title { get; private set; }

    private List<TuneTitle> _alternateTitles = new List<TuneTitle>();

    public IReadOnlyList<TuneTitle> AlternateTitles =>
        _alternateTitles;

    public TuneTypeValueObj TuneType { get; private set; }
    public TuneKeyValueObj TuneKey { get; private set; }
    public TuneComposer Composer { get; private set; }
    public DateOnly DateAdded { get; private set; }
    
    private List<TrackTune> _featuredOnTrack = new List<TrackTune>();
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
        
       
        // if (!Enum.TryParse<TuneTypeEnum>(type, true, out var tuneType))
        //     throw new Exception();
        //
        // tune.TuneType = tuneType;

        // if (!Enum.TryParse<TuneKeyEnum>(key, true, out var tuneKey))
        //     throw new Exception();
        //
        // tune.TuneKey = tuneKey;

        return tune;

    }

    public Unit AddAlternateTitle(TuneTitle title)
    {
        _alternateTitles.Add(title);
        return Unit.Default;
    }

    public Boolean RemoveAlternateTitle(TuneTitle title)
    {
        return _alternateTitles.Remove(title);
        
    }

    public Tune Update(TuneTitle title, TuneComposer composer, TuneTypeValueObj type, TuneKeyValueObj key)
    {
        Title = title;
        Composer = composer;
        TuneType = type;
        TuneKey = key;
        
        // if (!Enum.TryParse<TuneTypeEnum>(type, true, out var tuneType))
        //     throw new Exception();
        //
        // this.TuneType = tuneType;

        // if (!Enum.TryParse<TuneKeyEnum>(key, true, out var tuneKey))
        //     throw new Exception();
        //
        // this.TuneKey = tuneKey;

        return this;
    }
}
