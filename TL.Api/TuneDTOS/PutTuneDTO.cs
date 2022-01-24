using TL.Domain;

namespace TL.Api.TuneDTOs;

public class PutTuneDTO
{
    public string Title { get; set; }
    public TuneTypeEnum Type { get; set; }
    public TuneKeyEnum Key { get; set; }
    public string Composer { get; set; }
    

    public static Tune UpdatedTune(Tune tune, PutTuneDTO dto)
    {
        tune.UpdateTitle(dto.Title);
        tune.UpdateComposer(dto.Composer);
        tune.UpdateType(dto.Type);
        tune.UpdateKey(dto.Key);
        
        return tune;
    }
}