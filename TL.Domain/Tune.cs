using TL.Domain.Exceptions;
using TL.Domain.Validators;
using TL.Domain.ValueObjects.TuneValueObjects;

namespace TL.Domain;

public class Tune
{
    private Tune()
    {
    }

    public int Id { get; private set; }
    public TuneTitle Title { get; private set; }

    private List<AlternateTitleInTune> _alternateTitles;

    public IReadOnlyList<TuneTitle> AlternateTitles =>
        _alternateTitles
            .Select(x => x.Title)
            .ToList();

    public TuneTypeEnum TuneType { get; private set; }
    public TuneKeyEnum TuneKey { get; private set; }
    public TuneComposer Composer { get; private set; }
    public DateOnly DateAdded { get; private set; }
    private List<TrackTune> _featuredOnTrack = new List<TrackTune>();
    public IReadOnlyList<TrackTune> FeaturedOnTrack => _featuredOnTrack;

    public static Tune CreateTune(TuneTitle title, TuneComposer composer, string type, string key)
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

    public void AddAlternateTitle(TuneTitle title)
    {
        _alternateTitles.Add(new AlternateTitleInTune(title, this));
    }

    public void RemoveAlternateTitle(TuneTitle title)
    {
        var titleValue = title.Value;
        var foundTitle = _alternateTitles.Find(x => x.Title.Value == titleValue);
        
        if (!_alternateTitles.Contains(foundTitle))
            throw new Exception();
        
        _alternateTitles.Remove(foundTitle);
    }

    public void Update(TuneTitle title, TuneComposer composer, TuneTypeEnum type, TuneKeyEnum key)
    {
        Title = title;
        Composer = composer;
        TuneType = type;
        TuneKey = key;
    }
}

public class AlternateTitleInTune
{
    public TuneTitle Title { get; protected set; }
    public Tune Tune { get; protected set; }

    public AlternateTitleInTune(TuneTitle title, Tune tune)
    {
        Title = title;
        Tune = tune;
    }
}