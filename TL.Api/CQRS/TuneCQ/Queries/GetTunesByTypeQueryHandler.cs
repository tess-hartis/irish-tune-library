using MediatR;
using Microsoft.EntityFrameworkCore;
using TL.Api.DTOs.TuneDTOS;
using TL.Domain;
using TL.Repository;

namespace TL.Api.CQRS.TuneCQ.Queries;

public class GetTunesByTypeQuery : IRequest<IEnumerable<GetTuneDTO>>
{
    public TuneTypeEnum Type { get; }

    public GetTunesByTypeQuery(TuneTypeEnum type)
    {
        Type = type;
    }
}

public class GetTunesByTypeQueryHandler : IRequestHandler<GetTunesByTypeQuery, IEnumerable<GetTuneDTO>>
{
    private readonly ITuneRepository _tuneRepository;

    public GetTunesByTypeQueryHandler(ITuneRepository tuneRepository)
    {
        _tuneRepository = tuneRepository;
    }

    public async Task<IEnumerable<GetTuneDTO>> Handle
        (GetTunesByTypeQuery request, CancellationToken cancellationToken)
    {
        var tunes = await _tuneRepository
            .GetByWhere(x => x.TuneType == request.Type).ToListAsync();
        return tunes.Select(GetTuneDTO.FromTune);
    }
}