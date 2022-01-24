using TL.Domain;

namespace TL.Api.TuneDTOs;

public class PostAltTitleDTO
{
    public string AlternateTitle { get; set; }

    public static Tune PostAlternateTitle(Tune tune, PostAltTitleDTO dto)
    {
        tune.AddAlternateTitle(dto.AlternateTitle);
        return tune;
    }
}