using TL.Domain;
using TL.Domain.ValueObjects.TrackValueObjects;

namespace TL.Api.DTOs.TrackDTOs;

public class PutTrackDTO
{
    public string Title { get; set; }
    public int Number { get; set; }

    public static Track UpdatedTrack(Track track, PutTrackDTO dto)
    {
        var title = TrackTitle.Create(dto.Title);
        var trackNumber = TrackNumber.Create(dto.Number);
        track.Update(title, trackNumber);
        return track;
    }
}