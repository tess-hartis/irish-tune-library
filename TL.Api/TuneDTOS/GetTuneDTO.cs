using TL.Domain;
using TL.Domain.ValueObjects.TuneValueObjects;

namespace TL.Api.TuneDTOs;

public class GetTuneDTO
{
    public string Title { get; set; }
    public int Id { get; set; }
    public TuneTypeEnum Type { get; set; }
    public TuneKeyEnum Key { get; set; }
    public string Composer { get; set; }
    public List<TuneTitle> AlternateTitles { get; set; }

    public static GetTuneDTO FromTune(Tune tune)
    {
        return new GetTuneDTO
        {
            Title = tune.Title.Value,
            Id = tune.Id,
            Type = tune.TuneType,
            Key = tune.TuneKey,
            Composer = tune.Composer,
            AlternateTitles = tune.AlternateTitles
        };
    }
}