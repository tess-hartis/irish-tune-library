using TL.Domain.Exceptions;
using TL.Domain.Validators;
using TL.Domain.ValueObjects.TuneValueObjects;

namespace TL.Domain;

public class Tune
{
    private Tune(){ }
    
    public int Id { get; private set; }
    public TuneTitle Title { get;  private set; }
    private List<TuneTitle> _alternateTitles = new List<TuneTitle>();
    public List<TuneTitle> AlternateTitles => _alternateTitles;
    public TuneTypeEnum TuneType { get; private set; }
    public TuneKeyEnum TuneKey { get; private set; }
    public string Composer { get; private set; }
    public DateOnly DateAdded { get; private set; }
    private List<TrackTune> _featuredOnTrack = new List<TrackTune>();
    public IReadOnlyList<TrackTune> FeaturedOnTrack => _featuredOnTrack;
    
    public static Tune CreateTune(TuneTitle title, string composer, string type, string key)
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

        _alternateTitles.Add(title);
        
    }

    public void RemoveAlternateTitle(TuneTitle title)
    {
        if (!_alternateTitles.Contains(title))
            throw new InvalidOperationException($"The title {title} was not found");
        
        _alternateTitles.Remove(title);
    }
    
    public void Update(TuneTitle title, string composer, TuneTypeEnum type, TuneKeyEnum key)
    {
        Title = title;
        Composer = composer;
        TuneType = type;
        TuneKey = key;
    }
    
    
}