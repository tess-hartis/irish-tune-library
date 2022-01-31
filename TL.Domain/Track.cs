using System.ComponentModel.DataAnnotations;
using FluentValidation;
using TL.Common;
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
    public int AlbumId { get; private set; }

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
        _tuneList.Add(tune);
    }
    
    public void RemoveTune(Tune tune)
    {
        _tuneList.Remove(tune);
    }
    
    public void UpdateTitle(string title)
    {
        Title = title;
    }

    public void UpdateTrackNumber(int trackNumber)
    {
        TrackNumber = trackNumber;
    }
    
}