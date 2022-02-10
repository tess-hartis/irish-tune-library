using MediatR;
using Microsoft.EntityFrameworkCore;
using TL.Api.DTOs.TuneDTOS;
using TL.Repository;

namespace TL.Api.CQRS.TuneCQ.Queries;

public record GetAllTunesQuery : IRequest<IEnumerable<GetTuneDTO>>;

public class GetAllTunesQueryHandler : IRequestHandler<GetAllTunesQuery, IEnumerable<GetTuneDTO>>
{
    private readonly ITuneRepository _tuneRepository;

    public GetAllTunesQueryHandler(ITuneRepository tuneRepository)
    {
        _tuneRepository = tuneRepository;
    }

    public async Task<IEnumerable<GetTuneDTO>> Handle
        (GetAllTunesQuery request, CancellationToken cancellationToken)
    {
        var tunes = await _tuneRepository.GetEntities().ToListAsync();
        return tunes.Select(GetTuneDTO.FromTune);
    }
}