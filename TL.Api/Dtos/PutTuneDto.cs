using TL.Domain;
using TL.Repository;

namespace TL.Api.Dtos;

public class PutTuneDto
{
    public string Title { get; set; }
    public TuneTypeEnum Type { get; set; }
    public TuneKeyEnum Key { get; set; }
    public string Composer { get; set; }
    public List<string> AlternateTitles { get; set; }
    
    public static Tune UpdatedTune(Tune tune, PutTuneDto dto)
    {
        tune.Title = dto.Title;
        tune.TuneType = dto.Type;
        tune.TuneKey = dto.Key;
        tune.Composer = dto.Composer;
        tune.AlternateTitles = dto.AlternateTitles;
        return tune;
    }
}