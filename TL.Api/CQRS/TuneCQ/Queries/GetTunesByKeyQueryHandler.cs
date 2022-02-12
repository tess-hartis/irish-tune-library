using MediatR;
using Microsoft.EntityFrameworkCore;
using TL.Api.DTOs.TuneDTOS;
using TL.Domain;
using TL.Repository;

namespace TL.Api.CQRS.TuneCQ.Queries;

public class GetTunesByKeyQuery : IRequest<IEnumerable<Tune>>
{
    public TuneKeyEnum Key { get; }

    public GetTunesByKeyQuery(TuneKeyEnum key)
    {
        Key = key;
    }
}

public class GetTunesByKeyQueryHandler : IRequestHandler<GetTunesByKeyQuery, IEnumerable<Tune>>
{
    private readonly ITuneRepository _tuneRepository;

    public GetTunesByKeyQueryHandler(ITuneRepository tuneRepository)
    {
        _tuneRepository = tuneRepository;
    }

    public async Task<IEnumerable<Tune>> Handle(GetTunesByKeyQuery request, CancellationToken cancellationToken)
    {
        var tunes = await _tuneRepository
            .GetByWhere(x => x.TuneKey == request.Key).ToListAsync();
        return tunes;
    }
}