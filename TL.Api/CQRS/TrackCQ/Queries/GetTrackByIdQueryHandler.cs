using MediatR;
using TL.Api.DTOs.TrackDTOs;
using TL.Domain;
using TL.Repository;

namespace TL.Api.CQRS.TrackCQ.Queries;

public class GetTrackByIdQuery : IRequest<Track>
{
    public int Id { get; }
    
    public GetTrackByIdQuery(int id)
    {
        Id = id;
    }
    
}
public class GetTrackByIdQueryHandler : IRequestHandler<GetTrackByIdQuery, Track>
{
    private readonly ITrackRepository _trackRepository;

    public GetTrackByIdQueryHandler(ITrackRepository trackRepository)
    {
        _trackRepository = trackRepository;
    }

    public async Task<Track> Handle(GetTrackByIdQuery request, CancellationToken cancellationToken)
    {
        var track = await _trackRepository.FindAsync(request.Id);
        return track;

    }
}