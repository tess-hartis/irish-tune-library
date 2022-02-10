using TL.Domain;
using TL.Domain.ValueObjects.TuneValueObjects;

namespace TL.Api.DTOs.TuneDTOS;

public class PutTuneDTO
{
    public string Title { get; set; }
    public string Composer { get; set; }
    public string Type { get; set; }
    public string Key { get; set; }
    
    public static Tune UpdatedTune(Tune tune, PutTuneDTO dto)
    {
        var title = TuneTitle.Create(dto.Title);
        var composer = TuneComposer.Create(dto.Composer);
        tune.Update(title, composer, dto.Type, dto.Key);
        
        return tune;
    }
}