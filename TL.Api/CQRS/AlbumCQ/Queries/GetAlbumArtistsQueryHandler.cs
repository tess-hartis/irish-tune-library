using MediatR;
using TL.Api.DTOs.ArtistDTOs;
using TL.Repository;

namespace TL.Api.CQRS.AlbumCQ.Queries;

public class GetAlbumArtistsQuery : IRequest<IEnumerable<GetArtistDTO>>
{
    public int AlbumId { get; }

    public GetAlbumArtistsQuery(int albumId)
    {
        AlbumId = albumId;
    }
}

public class GetAlbumArtistsQueryHandler : IRequestHandler<GetAlbumArtistsQuery, IEnumerable<GetArtistDTO>>
{
    private readonly IAlbumArtistService _albumArtistService;

    public GetAlbumArtistsQueryHandler(IAlbumArtistService albumArtistService)
    {
        _albumArtistService = albumArtistService;
    }

    public async Task<IEnumerable<GetArtistDTO>> Handle
        (GetAlbumArtistsQuery request, CancellationToken cancellationToken)
    {
        var artists = await _albumArtistService.FindAlbumArtists(request.AlbumId);
        return artists.Select(GetArtistDTO.FromArtist);
        
    }
}