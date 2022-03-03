using MediatR;
using Microsoft.EntityFrameworkCore;
using TL.Domain;
using TL.Repository;

namespace TL.CQRS.TuneCQ.Queries;

public class GetTunesByTypeKeyQuery : IRequest<IEnumerable<Tune>>
{
    public string Type { get; }
    public string Key { get; }

    public GetTunesByTypeKeyQuery(string type, string key)
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
            .GetByWhere(x => x.TuneType.Value == request.Type & 
                             x.TuneKey.Value == request.Key)
            .ToListAsync();
        return tunes;
    }
}