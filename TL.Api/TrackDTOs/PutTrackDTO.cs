using TL.Domain;

namespace TL.Api.TrackDTOs;

public class PutTrackDTO
{
    public string Title { get; set; }
    public int TrackNumber { get; set; }

    public static Track UpdatedTrack(Track track, PutTrackDTO dto)
    {
        track.Title = dto.Title;
        track.TrackNumber = dto.TrackNumber;
        return track;
    }
}