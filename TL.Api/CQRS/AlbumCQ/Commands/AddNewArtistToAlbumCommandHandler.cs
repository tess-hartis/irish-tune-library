using MediatR;
using TL.Api.DTOs.AlbumDTOs;
using TL.Domain.ValueObjects.ArtistValueObjects;
using TL.Repository;

namespace TL.Api.CQRS.AlbumCQ.Commands;

public class AddNewArtistToAlbumCommand : IRequest<GetAlbumDTO>
{
    public int AlbumId { get; }
    public ArtistName Name { get; }

    public AddNewArtistToAlbumCommand(int albumId, ArtistName name)
    {
        AlbumId = albumId;
        Name = name;
    }
}
public class AddNewArtistToAlbumCommandHandler : IRequestHandler<AddNewArtistToAlbumCommand, GetAlbumDTO>
{
    private readonly IAlbumArtistService _albumArtistService;

    public AddNewArtistToAlbumCommandHandler(IAlbumArtistService albumArtistService)
    {
        _albumArtistService = albumArtistService;
    }

    public async Task<GetAlbumDTO> Handle(AddNewArtistToAlbumCommand command, CancellationToken cancellationToken)
    {
        var album = await _albumArtistService.AddNewArtistToAlbum(command.AlbumId, command.Name);
        return GetAlbumDTO.FromAlbum(album);
        
        //need to edit GetAlbumDTO to also include Album Artists
    }
}