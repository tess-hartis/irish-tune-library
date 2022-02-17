using LanguageExt;
using MediatR;
using TL.Api.DTOs.AlbumDTOs;
using TL.Domain;
using TL.Repository;
using Unit = LanguageExt.Unit;

namespace TL.Api.CQRS.AlbumCQ.Commands;

public class AddExistingArtistToAlbumCommand : IRequest<Option<Album>>
{
    public int AlbumId { get; }
    public int ArtistId { get; }

    public AddExistingArtistToAlbumCommand(int albumId, int artistId)
    {
        AlbumId = albumId;
        ArtistId = artistId;
    }
}
public class AddExistingArtistToAlbumCommandHandler : IRequestHandler<AddExistingArtistToAlbumCommand, Option<Album>>
{
    private readonly IAlbumArtistService _albumArtistService;

    public AddExistingArtistToAlbumCommandHandler(IAlbumArtistService albumArtistService)
    {
        _albumArtistService = albumArtistService;
    }

    public async Task<Option<Album>> Handle(AddExistingArtistToAlbumCommand command, CancellationToken cancellationToken)
    {
        return await _albumArtistService.AddExistingArtistToAlbum(command.AlbumId, command.ArtistId);
    }
}