using TL.Domain;

namespace TL.Api.TrackDTOs;

public class GetTrackDTO
{
    public string Title { get; set; }
    
    public int TrackNumber { get; set; }
    
    public int Id { get; set; }

    public static GetTrackDTO FromTrack(Track track)
    {
        return new GetTrackDTO()
        {
            Title = track.Title,
            TrackNumber = track.TrackNumber,
            Id = track.Id
            
        };
    }
}