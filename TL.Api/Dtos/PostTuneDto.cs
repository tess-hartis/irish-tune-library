using Microsoft.AspNetCore.JsonPatch;
using TL.Domain;
using TL.Repository;

namespace TL.Api.Dtos;

public class PostTuneDto
{
    public string Title { get; set; }
    public TuneTypeEnum Type { get; set; }
    public TuneKeyEnum Key { get; set; }
    public string Composer { get; set; }

    public static Tune ToTune(PostTuneDto dto)
    {
        return new Tune(dto.Title, dto.Type, dto.Key, dto.Composer);
    }
    
}