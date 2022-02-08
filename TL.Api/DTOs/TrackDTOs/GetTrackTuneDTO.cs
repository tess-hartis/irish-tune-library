using TL.Domain;

namespace TL.Api.DTOs.TrackDTOs;

public class GetTrackTuneDTO
{
    public int TuneId { get; set; }
    public string Title { get; set; }
    public int Order { get; set; }

    public static GetTrackTuneDTO FromTrackTune(TrackTune trackTune)
    {
        return new GetTrackTuneDTO
        {
            TuneId = trackTune.TuneId,
            Title = trackTune.Title,
            Order = trackTune.Order.Value
        };
    }
}