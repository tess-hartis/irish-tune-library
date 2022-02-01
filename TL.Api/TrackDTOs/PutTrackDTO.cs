using TL.Domain;

namespace TL.Api.TrackDTOs;

public class PutTrackDTO
{
    public string Title { get; set; }
    public int TrackNumber { get; set; }

    public static Track UpdatedTrack(Track track, PutTrackDTO dto)
    {
        track.Update(dto.Title, dto.TrackNumber);
        return track;
    }
}