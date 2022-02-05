using TL.Domain;
using TL.Domain.ValueObjects.TuneValueObjects;

namespace TL.Api.TuneDTOs;

public class PostTuneDTO
{
    public string Title { get; set; }
    public string Type { get; set; }
    public string Key { get; set; }
    public string Composer { get; set; }

    public static Tune Create(PostTuneDTO dto)
    {
        var title = TuneTitle.Create(dto.Title);
        var tune = Tune.CreateTune(title, dto.Composer, dto.Type, dto.Key);
        return tune;
    }
}