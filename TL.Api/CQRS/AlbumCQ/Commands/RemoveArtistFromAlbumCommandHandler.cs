using MediatR;
using TL.Api.DTOs.AlbumDTOs;
using TL.Domain;
using TL.Repository;

namespace TL.Api.CQRS.AlbumCQ.Commands;

public class RemoveArtistFromAlbumCommand : IRequest<Album>
{
    public int AlbumId { get; }
    public int ArtistId { get; }

    public RemoveArtistFromAlbumCommand(int albumId, int artistId)
    {
        AlbumId = albumId;
        ArtistId = artistId;
    }
}
public class RemoveArtistFromAlbumCommandHandler : IRequestHandler<RemoveArtistFromAlbumCommand, Album>
{
    private readonly IAlbumArtistService _albumArtistService;
    private readonly IAlbumRepository _albumRepository;

    public RemoveArtistFromAlbumCommandHandler(IAlbumArtistService albumArtistService, IAlbumRepository albumRepository)
    {
        _albumArtistService = albumArtistService;
        _albumRepository = albumRepository;
    }

    public async Task<Album> Handle(RemoveArtistFromAlbumCommand command, CancellationToken cancellationToken)
    {
        var album = await _albumArtistService.RemoveArtistFromAlbum(command.AlbumId, command.ArtistId);
        return album;

    }
}