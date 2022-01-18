using TL.Domain;

namespace TL.Api.TuneDTOS;

public class PostTuneDTO
{
    public string Title { get; set; }
    public TuneTypeEnum Type { get; set; }
    public TuneKeyEnum Key { get; set; }
    public string Composer { get; set; }

    public static Tune ToTune(PostTuneDTO dto)
    {
        return new Tune(dto.Title, dto.Type, dto.Key, dto.Composer);
    }
    
}