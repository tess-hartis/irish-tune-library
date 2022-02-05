using TL.Domain;
using TL.Domain.ValueObjects.TuneValueObjects;

namespace TL.Api.TuneDTOs;

public class PutTuneDTO
{
    public string Title { get; set; }
    public TuneTypeEnum Type { get; set; }
    public TuneKeyEnum Key { get; set; }
    public string Composer { get; set; }
    

    public static Tune UpdatedTune(Tune tune, PutTuneDTO dto)
    {
        var title = TuneTitle.Create(dto.Title);
        tune.Update(title, dto.Composer, dto.Type, dto.Key);
        
        return tune;
    }
}