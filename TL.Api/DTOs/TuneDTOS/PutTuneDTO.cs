using TL.Domain;
using TL.Domain.ValueObjects.TuneValueObjects;

namespace TL.Api.DTOs.TuneDTOS;

public class PutTuneDTO
{
    public string Title { get; set; }
    public string Composer { get; set; }
    public string Type { get; set; }
    public string Key { get; set; }
    
}