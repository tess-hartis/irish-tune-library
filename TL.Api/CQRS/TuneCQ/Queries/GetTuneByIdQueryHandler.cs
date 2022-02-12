using MediatR;
using TL.Api.DTOs.TuneDTOS;
using TL.Domain;
using TL.Repository;

namespace TL.Api.CQRS.TuneCQ.Queries;

public class GetTuneByIdQuery : IRequest<Tune>
{
    public int Id { get; }

    public GetTuneByIdQuery(int id)
    {
        Id = id;
    }
}

public class GetTuneByIdQueryHandler : IRequestHandler<GetTuneByIdQuery, Tune>
{
    private readonly ITuneRepository _tuneRepository;

    public GetTuneByIdQueryHandler(ITuneRepository tuneRepository)
    {
        _tuneRepository = tuneRepository;
    }

    public async Task<Tune> Handle
        (GetTuneByIdQuery request, CancellationToken cancellationToken)
    {
        var tune = await _tuneRepository.FindAsync(request.Id);
        return tune;

    }
}