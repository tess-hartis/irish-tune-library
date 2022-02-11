using MediatR;
using TL.Api.DTOs.AlbumDTOs;
using TL.Repository;

namespace TL.Api.CQRS.AlbumCQ.Commands;

public class AddExistingArtistToAlbumCommand : IRequest<GetAlbumDTO>
{
    public int AlbumId { get; }
    public int ArtistId { get; }

    public AddExistingArtistToAlbumCommand(int albumId, int artistId)
    {
        AlbumId = albumId;
        ArtistId = artistId;
    }
}
public class AddExistingArtistToAlbumCommandHandler : IRequestHandler<AddExistingArtistToAlbumCommand, GetAlbumDTO>
{
    private readonly IAlbumArtistService _albumArtistService;
    private readonly IAlbumRepository _albumRepository;

    public AddExistingArtistToAlbumCommandHandler(IAlbumArtistService albumArtistService, IAlbumRepository albumRepository)
    {
        _albumArtistService = albumArtistService;
        _albumRepository = albumRepository;
    }

    public async Task<GetAlbumDTO> Handle(AddExistingArtistToAlbumCommand command, CancellationToken cancellationToken)
    {
        var album = await _albumArtistService.AddExistingArtistToAlbum(command.AlbumId, command.ArtistId);
        return GetAlbumDTO.FromAlbum(album);
    }
}