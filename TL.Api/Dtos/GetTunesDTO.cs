using TL.Domain;

namespace TL.Api.Dtos;

public class GetTunesDTO
{
    private List<GetTuneDTO> _tunes { get; } = new List<GetTuneDTO>();
    public IEnumerable<GetTuneDTO> AllTunes => _tunes;

    public static GetTunesDTO GetAll(IEnumerable<Tune> tunes)
    {
        var dto = new GetTunesDTO();
        foreach (var tune in tunes)
        {
            var tuneDto = GetTuneDTO.FromTune(tune);
            dto._tunes.Add(tuneDto);
        }

        return dto;
    }
}