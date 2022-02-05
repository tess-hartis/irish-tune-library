using TL.Domain;
using TL.Domain.ValueObjects.TrackValueObjects;

namespace TL.Api.TrackDTOs;

public class PostTrackDTO
{
    public string Title { get; set; }
    public int TrackNumber { get; set; }

    public static Track ToTrack(PostTrackDTO dto)
    {
        var title = TrackTitle.Create(dto.Title);
        var track = Track.CreateTrack(title, dto.TrackNumber);
        return track;
    }
}