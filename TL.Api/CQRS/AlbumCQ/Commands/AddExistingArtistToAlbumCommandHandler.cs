using LanguageExt;
using static LanguageExt.Prelude;
using MediatR;
using TL.Api.DTOs.AlbumDTOs;
using TL.Data;
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
    private readonly TuneLibraryContext _context;
    private IAlbumRepository _albumRepository;
    private IArtistRepository _artistRepository;
    

    public AddExistingArtistToAlbumCommandHandler(TuneLibraryContext context)
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
    
    public async Task<Option<Unit>> Handle(AddExistingArtistToAlbumCommand command, CancellationToken cancellationToken)
    {
        
        var album = await AlbumRepo.FindAsync(command.AlbumId);
        var artist = await ArtistRepo.FindAsync(command.ArtistId);
        
        var result = 
            from al in album 
            from ar in artist 
            select al.AddArtist(ar);
        
        ignore(result.Map(async _ => await _context.SaveChangesAsync()));
        
        return result;

    }
}