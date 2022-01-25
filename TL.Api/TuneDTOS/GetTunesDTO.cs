using TL.Domain;

namespace TL.Api.TuneDTOs;

public class GetTunesDTO
{
    // private List<GetTuneDTO> _tunes { get; } = new List<GetTuneDTO>();
    // public IEnumerable<GetTuneDTO> AllTunes => _tunes;

    public List<GetTuneDTO> Tunes = new List<GetTuneDTO>();
    public static List<GetTuneDTO> GetAll(IEnumerable<Tune> tunes)
    {
        var dto = new GetTunesDTO();
        foreach (var tune in tunes)
        {
            var tuneDto = GetTuneDTO.FromTune(tune);
            dto.Tunes.Add(tuneDto);
        }
    
        return dto.Tunes;
    }
}