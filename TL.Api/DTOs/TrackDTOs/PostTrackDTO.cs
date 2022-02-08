using TL.Domain;
using TL.Domain.ValueObjects.TrackValueObjects;

namespace TL.Api.DTOs.TrackDTOs;

public class PostTrackDTO
{
    public string Title { get; set; }
    public int Number { get; set; }

    public static Track ToTrack(PostTrackDTO dto)
    {
        var title = TrackTitle.Create(dto.Title);
        var trackNumber = TrackNumber.Create(dto.Number);
        var track = Track.CreateTrack(title, trackNumber);
        return track;
    }
}