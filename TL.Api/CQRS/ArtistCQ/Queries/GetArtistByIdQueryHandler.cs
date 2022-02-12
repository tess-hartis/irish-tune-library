using MediatR;
using TL.Api.DTOs.ArtistDTOs;
using TL.Domain;
using TL.Repository;

namespace TL.Api.CQRS.ArtistCQ.Queries;

public class GetArtistByIdQuery : IRequest<Artist>
{
    public int Id { get; }

    public GetArtistByIdQuery(int id)
    {
        Id = id;
    }
}
public class GetArtistByIdQueryHandler : IRequestHandler<GetArtistByIdQuery, Artist>
{
    private readonly IArtistRepository _artistRepository;

    public GetArtistByIdQueryHandler(IArtistRepository artistRepository)
    {
        _artistRepository = artistRepository;
    }

    public async Task<Artist> Handle
        (GetArtistByIdQuery request, CancellationToken cancellationToken)
    {
        var artist = await _artistRepository.FindAsync(request.Id);
        return artist;

    }
}