using FluentValidation;
using TL.Domain.Exceptions;
using TL.Domain.Validators;

namespace TL.Domain;

public class Track
{
    private Track(){}
    public int Id { get; private set; }
    public string Title { get; private set; }
    public int TrackNumber { get; private set; }
    private List<Tune> _tuneList = new List<Tune>();
    public IReadOnlyList<Tune> TuneList => _tuneList;
    
    public int AlbumId { get; set; }
    public Album Album { get; set; }

    public static Track CreateTrack(string title, int trackNumber)
    {
        var track = new Track()
        {
            Title = title,
            TrackNumber = trackNumber
        };

        var validator = new TrackValidator();
        var errors = new List<string>();
        var results = validator.Validate(track);
        
        if (results.IsValid == false)
        {
            foreach (var validationFailure in results.Errors)
            {
                errors.Add($"{validationFailure.ErrorMessage}");
            }
            
            throw new InvalidEntityException(string.Join(", ", errors));
        }

        return track;
    }
    
    public void AddTune(Tune tune)
    {
        if (_tuneList.Contains(tune))
            throw new InvalidOperationException("The specified tune already exists on the track");
        
        _tuneList.Add(tune);
    }
    
    public void RemoveTune(Tune tune)
    {
        if (!_tuneList.Contains(tune))
            throw new InvalidOperationException("The specified tune was not found on the track");
        
        _tuneList.Remove(tune);
    }

    public void Update(string title, int trackNumber)
    {
        var errors = new List<string?>();
        
        if(!title.IsValidNameOrTitle())
            errors.Add("title must be between 2 and 75 characters");

        if (!trackNumber.IsValidTrackNum())
            errors.Add("invalid track number");

        if (errors.Any())
            throw new InvalidEntityException(string.Join(", ", errors));

        Title = title;
        TrackNumber = trackNumber;

    }
    
}