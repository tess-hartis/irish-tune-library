using LanguageExt;
using MediatR;
using TL.Api.DTOs.TuneDTOS;
using TL.Domain;
using TL.Repository;

namespace TL.Api.CQRS.TuneCQ.Queries;

public class GetTuneByIdQuery : IRequest<Option<Tune>>
{
    public int Id { get; }

    public GetTuneByIdQuery(int id)
    {
        Id = id;
    }
}

public class GetTuneByIdQueryHandler : IRequestHandler<GetTuneByIdQuery, Option<Tune>>
{
    private readonly ITuneRepository _tuneRepository;

    public GetTuneByIdQueryHandler(ITuneRepository tuneRepository)
    {
        _tuneRepository = tuneRepository;
    }

    public async Task<Option<Tune>> Handle
        (GetTuneByIdQuery request, CancellationToken cancellationToken)
    {
        return await _tuneRepository.FindAsync(request.Id);
    }
}