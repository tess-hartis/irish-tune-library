using MediatR;
using TL.Api.DTOs.AlbumDTOs;
using TL.Repository;

namespace TL.Api.CQRS.AlbumCQ.Commands;

public class RemoveArtistFromAlbumCommand : IRequest<GetAlbumDTO>
{
    public int AlbumId { get; }
    public int ArtistId { get; }

    public RemoveArtistFromAlbumCommand(int albumId, int artistId)
    {
        AlbumId = albumId;
        ArtistId = artistId;
    }
}
public class RemoveArtistFromAlbumCommandHandler : IRequestHandler<RemoveArtistFromAlbumCommand, GetAlbumDTO>
{
    private readonly IAlbumArtistService _albumArtistService;
    private readonly IAlbumRepository _albumRepository;

    public RemoveArtistFromAlbumCommandHandler(IAlbumArtistService albumArtistService, IAlbumRepository albumRepository)
    {
        _albumArtistService = albumArtistService;
        _albumRepository = albumRepository;
    }

    public async Task<GetAlbumDTO> Handle(RemoveArtistFromAlbumCommand command, CancellationToken cancellationToken)
    {
        var album = await _albumRepository.FindAsync(command.AlbumId);
        await _albumArtistService.RemoveArtistFromAlbum(command.AlbumId, command.ArtistId);
        return GetAlbumDTO.FromAlbum(album);
        
    }
}