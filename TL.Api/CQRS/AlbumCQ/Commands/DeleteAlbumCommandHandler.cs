using LanguageExt;
using static LanguageExt.Prelude;
using MediatR;
using TL.Repository;
using Unit = LanguageExt.Unit;


namespace TL.Api.CQRS.AlbumCQ.Commands;

public class DeleteAlbumCommand : IRequest<Option<Unit>>
{
    public int AlbumId { get; }

    public DeleteAlbumCommand(int albumId)
    {
        AlbumId = albumId;
    }
}
public class DeleteAlbumCommandHandler : IRequestHandler<DeleteAlbumCommand, Option<Unit>>
{
    private readonly IAlbumRepository _albumRepository;

    public DeleteAlbumCommandHandler(IAlbumRepository albumRepository)
    {
        _albumRepository = albumRepository;
    }

    public async Task<Option<Unit>> Handle(DeleteAlbumCommand command, CancellationToken cancellationToken)
    {
        var album = await _albumRepository.FindAsync(command.AlbumId);
        ignore(album.Map(async a => await _albumRepository.DeleteAsync(a)));
        return album.Map(a => unit);
    }
}