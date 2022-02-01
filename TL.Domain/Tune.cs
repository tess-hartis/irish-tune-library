using System.ComponentModel.DataAnnotations;
using TL.Common;
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
    
    public static Tune CreateTune(string title, string composer, TuneTypeEnum type, TuneKeyEnum key)
    {
        var tune = new Tune
        {
            Title = title,
            TuneType = type,
            TuneKey = key,
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
            
            throw new InvalidEntityException(string.Join(", ", errors));
        }
        
        return tune;
    }

    public void AddAlternateTitle(string title)
    {
        var errors = new List<string?>();
        
        if (string.IsNullOrWhiteSpace(title))
            errors.Add("title cannot be empty");
        
        if (title.Length < 2)
            errors.Add("title must be between 2 and 75 characters");
        
        if (title.Length > 75)
            errors.Add("title must be between 2 and 75 characters");
        
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
        
        if (string.IsNullOrWhiteSpace(composer))
            errors.Add("title cannot be empty");
        
        if (composer.Length < 2)
            errors.Add("title must be between 2 and 75 characters");
        
        if (composer.Length > 75)
            errors.Add("title must be between 2 and 75 characters");
        
        if (string.IsNullOrWhiteSpace(title))
            errors.Add("title cannot be empty");
        
        if (title.Length < 2)
            errors.Add("title must be between 2 and 75 characters");
        
        if (title.Length > 75)
            errors.Add("title must be between 2 and 75 characters");

        if (errors.Any())
            throw new InvalidEntityException(string.Join(", ", errors));

        Title = title;
        Composer = composer;
        TuneType = type;
        TuneKey = key;

    }
    
    
}