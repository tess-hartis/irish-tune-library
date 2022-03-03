using MediatR;
using Microsoft.EntityFrameworkCore;
using TL.Domain;
using TL.Repository;

namespace TL.Api.CQRS.TuneCQ.Queries;

public record GetAllTunesQuery : IRequest<IEnumerable<Tune>>;

public class GetAllTunesQueryHandler : IRequestHandler<GetAllTunesQuery, IEnumerable<Tune>>
{
    private readonly ITuneRepository _tuneRepository;

    public GetAllTunesQueryHandler(ITuneRepository tuneRepository)
    {
        _tuneRepository = tuneRepository;
    }

    public async Task<IEnumerable<Tune>> Handle
        (GetAllTunesQuery request, CancellationToken cancellationToken)
    {
        var tunes = await _tuneRepository.GetEntities().ToListAsync();
        return tunes;
    }
}