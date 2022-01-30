using System.Net;
using System.Xml;
using TL.Domain;

namespace TL.Api.TuneDTOs;

public class PostTuneDTO
{
    public string Title { get; set; }
    public TuneTypeEnum Type { get; set; }
    public TuneKeyEnum Key { get; set; }
    public string Composer { get; set; }

    public static Tune Create(PostTuneDTO dto)
    {
        var tune = Tune.CreateTune(dto.Title, dto.Composer, dto.Type, dto.Key);
        return tune;
    }
    
}