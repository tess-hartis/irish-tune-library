using TL.Domain;

namespace TL.Api.TrackDTOs;

public class GetTrackDTO
{
    public string Title { get; set; }

    public static GetTrackDTO FromTrack(Track track)
    {
        return new GetTrackDTO()
        {
            Title = track.Title,
            
        };
    }
}