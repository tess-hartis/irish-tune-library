using TL.Domain;

namespace TL.Api.TrackDTOs;

public class PostTrackDTO
{
    public string Title { get; set; }
    public int TrackNumber { get; set; }

    public static Track ToTrack(PostTrackDTO dto)
    {
        return new Track(dto.Title, dto.TrackNumber);
    }
}