using TL.Domain;

namespace TL.Api.TrackDTOs;

public class GetTrackDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    // public Album Album { get; set; }
    public int TrackNumber { get; set; }
    
    public static GetTrackDTO FromTrack(Track track)
    {
        return new GetTrackDTO
        {
            Id = track.Id,
            Title = track.Title,
            // Album = track.Album,
            TrackNumber = track.TrackNumber
        };
    }
}