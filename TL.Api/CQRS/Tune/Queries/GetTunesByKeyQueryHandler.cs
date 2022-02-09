using MediatR;
using Microsoft.EntityFrameworkCore;
using TL.Api.DTOs.TuneDTOS;
using TL.Domain;
using TL.Repository;

namespace TL.Api.CQRS.Tune.Queries;

public class GetTunesByKeyQuery : IRequest<IEnumerable<GetTuneDTO>>
{
    public TuneKeyEnum Key { get; }

    public GetTunesByKeyQuery(TuneKeyEnum key)
    {
        Key = key;
    }
}

public class GetTunesByKeyQueryHandler : IRequestHandler<GetTunesByKeyQuery, IEnumerable<GetTuneDTO>>
{
    private readonly ITuneRepository _tuneRepository;

    public GetTunesByKeyQueryHandler(ITuneRepository tuneRepository)
    {
        _tuneRepository = tuneRepository;
    }

    public async Task<IEnumerable<GetTuneDTO>> Handle(GetTunesByKeyQuery request, CancellationToken cancellationToken)
    {
        var tunes = await _tuneRepository
            .GetByWhere(x => x.TuneKey == request.Key).ToListAsync();
        return tunes.Select(GetTuneDTO.FromTune);
    }
}