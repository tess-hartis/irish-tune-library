using LanguageExt;
using MediatR;
using TL.Repository;
using static LanguageExt.Prelude;

namespace TL.CQRS.AlbumCQ.Commands;

public class RemoveArtistFromAlbumCommand : IRequest<Option<bool>>
{
    public int AlbumId { get; }
    public int ArtistId { get; }

    public RemoveArtistFromAlbumCommand(int albumId, int artistId)
    {
        AlbumId = albumId;
        ArtistId = artistId;
    }
}
public class RemoveArtistFromAlbumCommandHandler : IRequestHandler<RemoveArtistFromAlbumCommand, Option<bool>>
{
    private readonly IAlbumArtistUnitOfWork _unitOfWork;
    
    public RemoveArtistFromAlbumCommandHandler(IAlbumArtistUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Option<bool>> Handle(RemoveArtistFromAlbumCommand command, CancellationToken cancellationToken)
    {
        var album = await _unitOfWork.AlbumRepo.FindAsync(command.AlbumId);
        var artist = await _unitOfWork.ArtistRepo.FindAsync(command.ArtistId);
        
        var result =
            from al in album
            from ar in artist
            select al.RemoveArtist(al.Artists.FirstOrDefault(x => x.Id == ar.Id));
        
        ignore(result.Map(async x => await _unitOfWork.Save()));
        
        return result;
    }
}