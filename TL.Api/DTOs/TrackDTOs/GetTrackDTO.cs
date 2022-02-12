using TL.Domain;

namespace TL.Api.DTOs.TrackDTOs;

public class GetTrackDTO
{
    public int TrackId { get; set; }
    public int AlbumId { get; set; }
    public string Title { get; set; }
    public int TrackNumber { get; set; }
    public IEnumerable<GetTrackTuneDTO> Tunes { get; set; } = new List<GetTrackTuneDTO>();

    public static GetTrackDTO FromTrack(Track track)
    {
        var tunes = track.TrackTunes.Select(GetTrackTuneDTO.FromTrackTune);
        
        return new GetTrackDTO
        {
            TrackId = track.Id,
            AlbumId = track.AlbumId,
            Title = track.Title.Value,
            TrackNumber = track.TrackNumber.Value,
            Tunes = tunes

        };
    }
}