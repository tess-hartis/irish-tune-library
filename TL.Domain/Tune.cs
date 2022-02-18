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

    public TuneTypeEnum TuneType { get; private set; }
    public TuneKeyEnum TuneKey { get; private set; }
    public TuneComposer Composer { get; private set; }
    public DateOnly DateAdded { get; private set; }
    
    private List<TrackTune> _featuredOnTrack = new List<TrackTune>();
    public IEnumerable<Track> FeaturedOnTrack => 
        _featuredOnTrack.Select(x => x.Track).ToList();

    public static Tune Create(TuneTitle title, TuneComposer composer, string type, string key)
    {

        var tune = new Tune
        {
            Title = title,
            Composer = composer,
            DateAdded = DateOnly.FromDateTime(DateTime.Today)
        };
        
       
        if (!Enum.TryParse<TuneTypeEnum>(type, true, out var tuneType))
            throw new Exception();

        tune.TuneType = tuneType;

        if (!Enum.TryParse<TuneKeyEnum>(key, true, out var tuneKey))
            throw new Exception();

        tune.TuneKey = tuneKey;

        return tune;

    }

    public Tune AddAlternateTitle(TuneTitle title)
    {
        _alternateTitles.Add(title);
        return this;
    }

    public Boolean RemoveAlternateTitle(TuneTitle title)
    {
        return _alternateTitles.Remove(title);

        
        //may need to change parameter back to string??

        // var deleteMe = _alternateTitles.Find(x => x.Value == title);
        // _alternateTitles.Remove(deleteMe);
        // return this;
    }

    public Tune Update(TuneTitle title, TuneComposer composer, string type, string key)
    {
        Title = title;
        Composer = composer;
        
        if (!Enum.TryParse<TuneTypeEnum>(type, true, out var tuneType))
            throw new Exception();

        this.TuneType = tuneType;

        if (!Enum.TryParse<TuneKeyEnum>(key, true, out var tuneKey))
            throw new Exception();

        this.TuneKey = tuneKey;

        return this;
    }
}
