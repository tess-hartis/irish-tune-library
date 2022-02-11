using MediatR;
using TL.Api.DTOs.TrackDTOs;
using TL.Repository;

namespace TL.Api.CQRS.TrackCQ.Queries;

public class GetTrackByIdQuery : IRequest<GetTrackDTO>
{
    public int Id { get; }
    
    public GetTrackByIdQuery(int id)
    {
        Id = id;
    }
    
}
public class GetTrackByIdQueryHandler : IRequestHandler<GetTrackByIdQuery, GetTrackDTO>
{
    private readonly ITrackRepository _trackRepository;

    public GetTrackByIdQueryHandler(ITrackRepository trackRepository)
    {
        _trackRepository = trackRepository;
    }

    public async Task<GetTrackDTO> Handle(GetTrackByIdQuery request, CancellationToken cancellationToken)
    {
        var track = await _trackRepository.FindAsync(request.Id);
        return GetTrackDTO.FromTrack(track);
        
    }
}