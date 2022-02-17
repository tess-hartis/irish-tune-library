using LanguageExt;
using static LanguageExt.Prelude;
using MediatR;
using TL.Api.DTOs.AlbumDTOs;
using TL.Domain;
using TL.Repository;

namespace TL.Api.CQRS.AlbumCQ.Commands;

public class RemoveArtistFromAlbumCommand : IRequest<Option<Album>>
{
    public int AlbumId { get; }
    public int ArtistId { get; }

    public RemoveArtistFromAlbumCommand(int albumId, int artistId)
    {
        AlbumId = albumId;
        ArtistId = artistId;
    }
}
public class RemoveArtistFromAlbumCommandHandler : IRequestHandler<RemoveArtistFromAlbumCommand, Option<Album>>
{
    private readonly IAlbumArtistService _albumArtistService;
    private readonly IAlbumRepository _albumRepository;

    public RemoveArtistFromAlbumCommandHandler(IAlbumArtistService albumArtistService, IAlbumRepository albumRepository)
    {
        _albumArtistService = albumArtistService;
        _albumRepository = albumRepository;
    }

    public async Task<Option<Album>> Handle(RemoveArtistFromAlbumCommand command, CancellationToken cancellationToken)
    {
        return await _albumArtistService.RemoveArtistFromAlbum(command.AlbumId, command.ArtistId);
    }
}