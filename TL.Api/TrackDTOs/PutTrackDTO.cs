using TL.Domain;
using TL.Domain.ValueObjects.TrackValueObjects;

namespace TL.Api.TrackDTOs;

public class PutTrackDTO
{
    public string Title { get; set; }
    public int TrackNumber { get; set; }

    public static Track UpdatedTrack(Track track, PutTrackDTO dto)
    {
        var title = TrackTitle.Create(dto.Title);
        track.Update(title, dto.TrackNumber);
        return track;
    }
}