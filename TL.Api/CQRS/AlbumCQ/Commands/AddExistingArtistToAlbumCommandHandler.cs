using LanguageExt;
using static LanguageExt.Prelude;
using MediatR;
using TL.Api.DTOs.AlbumDTOs;
using TL.Domain;
using TL.Repository;
using Unit = LanguageExt.Unit;

namespace TL.Api.CQRS.AlbumCQ.Commands;

public class AddExistingArtistToAlbumCommand : IRequest<Option<Unit>>
{
    public int AlbumId { get; }
    public int ArtistId { get; }

    public AddExistingArtistToAlbumCommand(int albumId, int artistId)
    {
        AlbumId = albumId;
        ArtistId = artistId;
    }
}
public class AddExistingArtistToAlbumCommandHandler : IRequestHandler<AddExistingArtistToAlbumCommand, Option<Unit>>
{
    private readonly IAlbumArtistService _albumArtistService;
    

    public AddExistingArtistToAlbumCommandHandler(IAlbumArtistService albumArtistService)
    {
        _albumArtistService = albumArtistService;
      
    }

    public async Task<Option<Unit>> Handle(AddExistingArtistToAlbumCommand command, CancellationToken cancellationToken)
    {
        return await _albumArtistService.AddExistingArtistToAlbum(command.AlbumId, command.ArtistId);
        
        
        // var album = await _albumRepository.FindAsync(command.AlbumId);
        // var artist = await _artistRepository.FindAsync(command.ArtistId);
        //
        // var result = 
        //     from al in album 
        //     from ar in artist 
        //     select al.AddArtist(ar);
        //
        // ignore(result.Map(async _ => await _albumRepository.SaveAsync()));
        //
        // return result;

    }
}