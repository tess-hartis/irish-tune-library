using LanguageExt;
using static LanguageExt.Prelude;
using MediatR;
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
    private readonly IAlbumArtistUnitOfWork _unitOfWork;
    

    public AddExistingArtistToAlbumCommandHandler(IAlbumArtistUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Option<Unit>> Handle(AddExistingArtistToAlbumCommand command, CancellationToken cancellationToken)
    {
        
        var album = await _unitOfWork.AlbumRepo.FindAsync(command.AlbumId);
        var artist = await _unitOfWork.ArtistRepo.FindAsync(command.ArtistId);
        
        var result = 
            from al in album 
            from ar in artist 
            select al.AddArtist(ar);
        
        ignore(result.Map(async _ => await _unitOfWork.Save()));
        
        return result;
    }
}