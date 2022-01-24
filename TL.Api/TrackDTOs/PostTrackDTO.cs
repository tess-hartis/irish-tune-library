using TL.Domain;

namespace TL.Api.TrackDTOs;

public class PostTrackDTO
{
    public string Title { get; set; }
    public int TrackNumber { get; set; }

    public static Track ToTrack(PostTrackDTO dto)
    {
        var track = Track.CreateTrack(dto.Title, dto.TrackNumber);
        return track;
    }
}