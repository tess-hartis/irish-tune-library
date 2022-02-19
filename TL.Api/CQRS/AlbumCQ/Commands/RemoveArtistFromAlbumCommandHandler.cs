using LanguageExt;
using static LanguageExt.Prelude;
using MediatR;
using TL.Data;
using TL.Repository;

namespace TL.Api.CQRS.AlbumCQ.Commands;

public class RemoveArtistFromAlbumCommand : IRequest<Option<Boolean>>
{
    public int AlbumId { get; }
    public int ArtistId { get; }

    public RemoveArtistFromAlbumCommand(int albumId, int artistId)
    {
        AlbumId = albumId;
        ArtistId = artistId;
    }
}
public class RemoveArtistFromAlbumCommandHandler : IRequestHandler<RemoveArtistFromAlbumCommand, Option<Boolean>>
{
    private readonly TuneLibraryContext _context;
    private IAlbumRepository _albumRepository;
    private IArtistRepository _artistRepository;
    

    public RemoveArtistFromAlbumCommandHandler(TuneLibraryContext context)
    {
        _context = context;
    }
    
    private IAlbumRepository AlbumRepo
    {
        get
        {
            return _albumRepository = new AlbumRepository(_context);
        }
    }

    private IArtistRepository ArtistRepo
    {
        get
        {
            return _artistRepository = new ArtistRepository(_context);
        }
    }

    public async Task<Option<Boolean>> Handle(RemoveArtistFromAlbumCommand command, CancellationToken cancellationToken)
    {
        var album = await AlbumRepo.FindAsync(command.AlbumId);
        var artist = await ArtistRepo.FindAsync(command.ArtistId);
        
        var result =
            from al in album
            from ar in artist
            select al.RemoveArtist(al.Artists.FirstOrDefault(x => x.Id == ar.Id));
        
        ignore(result.Map(async x => await _context.SaveChangesAsync()));
        
        return result;
    }
}