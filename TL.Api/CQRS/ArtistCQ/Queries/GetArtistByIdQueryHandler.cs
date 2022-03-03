using LanguageExt;
using MediatR;
using TL.Domain;
using TL.Repository;

namespace TL.Api.CQRS.ArtistCQ.Queries;

public class GetArtistByIdQuery : IRequest<Option<Artist>>
{
    public int Id { get; }

    public GetArtistByIdQuery(int id)
    {
        Id = id;
    }
}
public class GetArtistByIdQueryHandler : IRequestHandler<GetArtistByIdQuery, Option<Artist>>
{
    private readonly IArtistRepository _artistRepository;

    public GetArtistByIdQueryHandler(IArtistRepository artistRepository)
    {
        _artistRepository = artistRepository;
    }

    public async Task<Option<Artist>> Handle
        (GetArtistByIdQuery request, CancellationToken cancellationToken)
    {
        return await _artistRepository.FindAsync(request.Id);
    }
}