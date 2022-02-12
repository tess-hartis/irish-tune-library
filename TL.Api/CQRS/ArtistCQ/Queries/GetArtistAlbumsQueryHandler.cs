using MediatR;
using TL.Api.DTOs.AlbumDTOs;
using TL.Domain;
using TL.Repository;

namespace TL.Api.CQRS.ArtistCQ.Queries;

public class GetArtistAlbumsQuery : IRequest<IEnumerable<Album>>
{
    public int ArtistId { get; }

    public GetArtistAlbumsQuery(int artistId)
    {
        ArtistId = artistId;
    }
}

public class GetArtistAlbumsQueryHandler : IRequestHandler<GetArtistAlbumsQuery, IEnumerable<Album>>
{
    private readonly IAlbumArtistService _albumArtistService;

    public GetArtistAlbumsQueryHandler(IAlbumArtistService albumArtistService)
    {
        _albumArtistService = albumArtistService;
    }

    public async Task<IEnumerable<Album>> Handle
        (GetArtistAlbumsQuery request, CancellationToken cancellationToken)
    {
        var albums = await _albumArtistService.FindArtistAlbums(request.ArtistId);
        return albums;

    }
}