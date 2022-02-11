using MediatR;
using Microsoft.EntityFrameworkCore;
using TL.Api.DTOs.TrackDTOs;
using TL.Repository;

namespace TL.Api.CQRS.TrackCQ.Queries;

public record GetAllTracksQuery : IRequest<IEnumerable<GetTrackDTO>>;

public class GetAllTracksQueryHandler : IRequestHandler<GetAllTracksQuery, IEnumerable<GetTrackDTO>>
{
    private readonly ITrackRepository _trackRepository;

    public GetAllTracksQueryHandler(ITrackRepository trackRepository)
    {
        _trackRepository = trackRepository;
    }

    public async Task<IEnumerable<GetTrackDTO>> Handle
        (GetAllTracksQuery request, CancellationToken cancellationToken)
    {
        var tracks = await _trackRepository.GetEntities().ToListAsync();
        return tracks.Select(GetTrackDTO.FromTrack);
        
    }
}