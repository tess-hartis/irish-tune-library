using MediatR;
using TL.Api.DTOs.TuneDTOS;
using TL.Repository;

namespace TL.Api.CQRS.Tune.Queries;

public class GetTuneByIdQuery : IRequest<GetTuneDTO>
{
    public int Id { get; }

    public GetTuneByIdQuery(int id)
    {
        Id = id;
    }
}

public class GetTuneByIdQueryHandler : IRequestHandler<GetTuneByIdQuery, GetTuneDTO>
{
    private readonly ITuneRepository _tuneRepository;

    public GetTuneByIdQueryHandler(ITuneRepository tuneRepository)
    {
        _tuneRepository = tuneRepository;
    }

    public async Task<GetTuneDTO> Handle
        (GetTuneByIdQuery request, CancellationToken cancellationToken)
    {
        var tune = await _tuneRepository.FindAsync(request.Id);
        return GetTuneDTO.FromTune(tune);
        
    }
}