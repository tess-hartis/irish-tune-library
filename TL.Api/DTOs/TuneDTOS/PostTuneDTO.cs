using TL.Domain;
using TL.Domain.ValueObjects.TuneValueObjects;

namespace TL.Api.DTOs.TuneDTOS;

public class PostTuneDTO
{
    public string Title { get; set; }
    public string Type { get; set; }
    public string Key { get; set; }
    public string Composer { get; set; }

    public static Tune Create(PostTuneDTO dto)
    {
        var title = TuneTitle.Create(dto.Title);
        var composer = TuneComposer.Create(dto.Composer);
        var tune = Tune.Create(title, composer, dto.Type, dto.Key);
        return tune;
    }
}