using MediatR;
using TL.Api.DTOs.AlbumDTOs;
using TL.Repository;

namespace TL.Api.CQRS.ArtistCQ.Queries;

public class GetArtistAlbumsQuery : IRequest<IEnumerable<GetAlbumDTO>>
{
    public int ArtistId { get; }

    public GetArtistAlbumsQuery(int artistId)
    {
        ArtistId = artistId;
    }
}

public class GetArtistAlbumsQueryHandler : IRequestHandler<GetArtistAlbumsQuery, IEnumerable<GetAlbumDTO>>
{
    private readonly IAlbumArtistService _albumArtistService;

    public GetArtistAlbumsQueryHandler(IAlbumArtistService albumArtistService)
    {
        _albumArtistService = albumArtistService;
    }

    public async Task<IEnumerable<GetAlbumDTO>> Handle
        (GetArtistAlbumsQuery request, CancellationToken cancellationToken)
    {
        var albums = await _albumArtistService.FindArtistAlbums(request.ArtistId);
        return albums.Select(GetAlbumDTO.FromAlbum);
        
    }
}