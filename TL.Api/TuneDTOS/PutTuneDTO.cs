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
        tune.Update(dto.Title, dto.Composer, dto.Type, dto.Key);
        
        return tune;
    }
}