using MediatR;
using Microsoft.EntityFrameworkCore;
using TL.Api.DTOs.TuneDTOS;
using TL.Domain;
using TL.Repository;

namespace TL.Api.CQRS.TuneCQ.Queries;

public class GetTunesByTypeQuery : IRequest<IEnumerable<Tune>>
{
    public TuneTypeEnum Type { get; }

    public GetTunesByTypeQuery(TuneTypeEnum type)
    {
        Type = type;
    }
}

public class GetTunesByTypeQueryHandler : IRequestHandler<GetTunesByTypeQuery, IEnumerable<Tune>>
{
    private readonly ITuneRepository _tuneRepository;

    public GetTunesByTypeQueryHandler(ITuneRepository tuneRepository)
    {
        _tuneRepository = tuneRepository;
    }

    public async Task<IEnumerable<Tune>> Handle
        (GetTunesByTypeQuery request, CancellationToken cancellationToken)
    {
        var tunes = await _tuneRepository
            .GetByWhere(x => x.TuneType == request.Type).ToListAsync();
        return tunes;
    }
}