using LanguageExt;
using MediatR;
using TL.Api.DTOs.TrackDTOs;
using TL.Domain;
using TL.Repository;

namespace TL.Api.CQRS.TrackCQ.Queries;

public class GetTrackByIdQuery : IRequest<Option<Track>>
{
    public int Id { get; }
    
    public GetTrackByIdQuery(int id)
    {
        Id = id;
    }
    
}
public class GetTrackByIdQueryHandler : IRequestHandler<GetTrackByIdQuery, Option<Track>>
{
    private readonly ITrackRepository _trackRepository;

    public GetTrackByIdQueryHandler(ITrackRepository trackRepository)
    {
        _trackRepository = trackRepository;
    }

    public async Task<Option<Track>> Handle(GetTrackByIdQuery request, CancellationToken cancellationToken)
    {
        return await _trackRepository.FindAsync(request.Id);
        
    }
}