using TL.Domain;

namespace TL.Api.TrackDTOs;

public class GetTracksDTO
{
    public List<GetTrackDTO> Tracks = new List<GetTrackDTO>();
    public static List<GetTrackDTO> GetAll(IEnumerable<Track> tracks)
    {
        var dto = new GetTracksDTO();
        foreach (var track in tracks)
        {
            var trackDto = GetTrackDTO.FromTrack(track);
            dto.Tracks.Add(trackDto);
        }

        return dto.Tracks;
    }
}