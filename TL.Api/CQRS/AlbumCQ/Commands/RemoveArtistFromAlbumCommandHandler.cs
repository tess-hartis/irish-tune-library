using LanguageExt;
using static LanguageExt.Prelude;
using MediatR;
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
    private readonly IAlbumArtistService _albumArtistService;

    public RemoveArtistFromAlbumCommandHandler(IAlbumArtistService albumArtistService)
    {
        _albumArtistService = albumArtistService;
    }

    public async Task<Option<Boolean>> Handle(RemoveArtistFromAlbumCommand command, CancellationToken cancellationToken)
    {
        return await _albumArtistService.RemoveArtistFromAlbum(command.AlbumId, command.ArtistId);

        // var album = await _albumRepository.FindAsync(command.AlbumId);
        // var artist = await _artistRepository.FindAsync(command.ArtistId);
        //
        // var result =
        //     from al in album
        //     from ar in artist
        //     select al.RemoveArtist(al.Artists.FirstOrDefault(x => x.Id == ar.Id));
        //
        // ignore(result.Map(async x => await _albumRepository.SaveAsync()));
        //
        // return result;
    }
}