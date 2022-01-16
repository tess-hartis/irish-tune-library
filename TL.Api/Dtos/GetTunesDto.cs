using TL.Domain;

namespace TL.Api.Dtos;

public class GetTunesDto
{
    private List<GetTuneDto> _tunes { get; } = new List<GetTuneDto>();
    public IEnumerable<GetTuneDto> AllTunes => _tunes;

    public static GetTunesDto GetAll(IEnumerable<Tune> tunes)
    {
        var dto = new GetTunesDto();
        foreach (var tune in tunes)
        {
            var tuneDto = GetTuneDto.FromTune(tune);
            dto._tunes.Add(tuneDto);
        }

        return dto;
    }
}