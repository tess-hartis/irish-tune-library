using MediatR;
using Microsoft.EntityFrameworkCore;
using TL.Api.DTOs.TrackDTOs;
using TL.Domain;
using TL.Repository;

namespace TL.Api.CQRS.TrackCQ.Queries;

public record GetAllTracksQuery : IRequest<IEnumerable<Track>>;

public class GetAllTracksQueryHandler : IRequestHandler<GetAllTracksQuery, IEnumerable<Track>>
{
    private readonly ITrackRepository _trackRepository;

    public GetAllTracksQueryHandler(ITrackRepository trackRepository)
    {
        _trackRepository = trackRepository;
    }

    public async Task<IEnumerable<Track>> Handle
        (GetAllTracksQuery request, CancellationToken cancellationToken)
    {
        var tracks = await _trackRepository.GetAll();
        return tracks;

    }
}