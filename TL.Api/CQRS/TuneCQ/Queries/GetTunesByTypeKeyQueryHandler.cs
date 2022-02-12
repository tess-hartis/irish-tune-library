using MediatR;
using Microsoft.EntityFrameworkCore;
using TL.Api.DTOs.TuneDTOS;
using TL.Domain;
using TL.Repository;

namespace TL.Api.CQRS.TuneCQ.Queries;

public class GetTunesByTypeKeyQuery : IRequest<IEnumerable<Tune>>
{
    public TuneTypeEnum Type { get; }
    public TuneKeyEnum Key { get; }

    public GetTunesByTypeKeyQuery(TuneTypeEnum type, TuneKeyEnum key)
    {
        Type = type;
        Key = key;
    }
}

public class GetTunesByTypeKeyQueryHandler : IRequestHandler<GetTunesByTypeKeyQuery, IEnumerable<Tune>>
{
    private readonly ITuneRepository _tuneRepository;

    public GetTunesByTypeKeyQueryHandler(ITuneRepository tuneRepository)
    {
        _tuneRepository = tuneRepository;
    }

    public async Task<IEnumerable<Tune>> Handle
        (GetTunesByTypeKeyQuery request, CancellationToken cancellationToken)
    {
        var tunes = await _tuneRepository
            .GetByWhere(x => x.TuneType == request.Type & x.TuneKey == request.Key)
            .ToListAsync();
        return tunes;
    }
}