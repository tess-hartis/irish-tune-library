using TL.Domain;

namespace TL.Api.Dtos;

public class GetTuneDto
{
    public string Title { get; set; }
    public TuneTypeEnum Type { get; set; }
    public TuneKeyEnum Key { get; set; }
    public string Composer { get; set; }
    public List<string> AlternateTitles { get; set; }
    public List<Track> FeaturedOn { get; set; }

    public static GetTuneDto FromTune(Tune tune)
    {
        return new GetTuneDto
        {
            Title = tune.Title,
            Type = tune.TuneType,
            Key = tune.TuneKey,
            Composer = tune.Composer,
            AlternateTitles = tune.AlternateTitles,
            FeaturedOn = tune.FeaturedOn
        };
    }
}