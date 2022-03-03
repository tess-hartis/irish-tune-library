using TL.Domain;

namespace TL.Api.DTOs.TuneDTOS;

public class GetTuneDTO
{
    public string Title { get; set; }
    public int Id { get; set; }
    public string Type { get; set; }
    public string Key { get; set; }
    public string Composer { get; set; }
    public List<string> AlternateTitles { get; set; }

    public static GetTuneDTO FromTune(Tune tune)
    {
        return new GetTuneDTO
        {
            Title = tune.Title.Value,
            Id = tune.Id,
            Type = tune.TuneType.Value,
            Key = tune.TuneKey.Value,
            Composer = tune.Composer.Value,
            AlternateTitles = tune.AlternateTitles
                .Select(x => x.Value)
                .ToList()
        };
    }
}