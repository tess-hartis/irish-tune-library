using TL.Domain;

namespace TL.Api.TrackDTOS;

public class GetTracksDTO
{
    private   List<GetTrackDTO> _tracks { get; }
    public IEnumerable<GetTrackDTO> AllTracks => _tracks;

    public static GetTracksDTO GetTracks(IEnumerable<Track> tracks)
    {
        var dto = new GetTracksDTO();
        foreach (var track in tracks)
        {
            var trackDto = GetTrackDTO.FromTrack(track);
            dto._tracks.Add(trackDto);
        }

        return dto;
    }
}