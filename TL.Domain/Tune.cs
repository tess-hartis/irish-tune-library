using TL.Domain.Exceptions;
using TL.Domain.Validators;

namespace TL.Domain;

public class Tune
{
    private Tune(){ }
    
    public int Id { get; private set; }
    public string Title { get;  private set; }
    private List<string> _alternateTitles = new List<string>();
    public List<string> AlternateTitles => _alternateTitles;
    public TuneTypeEnum TuneType { get; private set; }
    public TuneKeyEnum TuneKey { get; private set; }
    public string Composer { get; private set; }
    public DateOnly DateAdded { get; private set; }
    private List<Track> _tracks = new List<Track>();
    public IReadOnlyList<Track> Tracks => _tracks;
    
    public static Tune CreateTune(string title, string composer, string type, string key)
    {
        var tune = new Tune
        {
            Title = title,
            Composer = composer,
            DateAdded = DateOnly.FromDateTime(DateTime.Today)
        };

        var validator = new TuneValidator();
        var errors = new List<string>();
        var results = validator.Validate(tune);
        
        if (results.IsValid == false)
        {
            foreach (var validationFailure in results.Errors)
            {
                errors.Add($"{validationFailure.ErrorMessage}");
            }
        }

        if (!Enum.TryParse<TuneTypeEnum>(type, true, out var tuneType))
            errors.Add("invalid tune type");
        
        tune.TuneType = tuneType;
        
        if(!Enum.TryParse<TuneKeyEnum>(key, true, out var tuneKey))
            errors.Add("invalid tune key");

        tune.TuneKey = tuneKey;
        
        if (errors.Any())
            throw new InvalidEntityException(string.Join(", ", errors));
        
        return tune;
    }

    public void AddAlternateTitle(string title)
    {
        var errors = new List<string?>();
        
        if (!title.IsValidNameOrTitle())
            errors.Add("Title must be between 2 and 75 characters");
        
        if(_alternateTitles.Contains(title))
            errors.Add("Alternate title already exists");

        if (errors.Any())
            throw new InvalidEntityException(string.Join(", ", errors));
        
        _alternateTitles.Add(title);
        
    }

    public void RemoveAlternateTitle(string title)
    {
        if (!_alternateTitles.Contains(title))
            throw new InvalidOperationException($"The title {title} was not found");
        
        _alternateTitles.Remove(title);
    }
    
    public void Update(string title, string composer, TuneTypeEnum type, TuneKeyEnum key)
    {
        var errors = new List<string>();
        
        if (!title.IsValidNameOrTitle())
            errors.Add("composer must be between 2 and 75 characters");
        
        if (!composer.IsValidNameOrTitle())
            errors.Add("title must be between 2 and 75 characters");

        if (errors.Any())
            throw new InvalidEntityException(string.Join(", ", errors));

        Title = title;
        Composer = composer;
        TuneType = type;
        TuneKey = key;

    }
    
    
}