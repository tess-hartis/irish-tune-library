using MediatR;
using Microsoft.EntityFrameworkCore;
using TL.Domain;
using TL.Repository;

namespace TL.CQRS.TuneCQ.Queries;

public class GetTunesByKeyQuery : IRequest<IEnumerable<Tune>>
{
    public string Key { get; }

    public GetTunesByKeyQuery(string key)
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
            .GetByWhere(x => x.TuneKey.Value == request.Key).ToListAsync();
        return tunes;
    }
}