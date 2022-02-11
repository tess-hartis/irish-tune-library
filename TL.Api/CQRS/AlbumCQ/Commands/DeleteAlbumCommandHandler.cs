using MediatR;
using TL.Repository;

namespace TL.Api.CQRS.AlbumCQ.Commands;

public class DeleteAlbumCommand : IRequest<Unit>
{
    public int AlbumId { get; }

    public DeleteAlbumCommand(int albumId)
    {
        AlbumId = albumId;
    }
}
public class DeleteAlbumCommandHandler : IRequestHandler<DeleteAlbumCommand, Unit>
{
    private readonly IAlbumRepository _albumRepository;

    public DeleteAlbumCommandHandler(IAlbumRepository albumRepository)
    {
        _albumRepository = albumRepository;
    }

    public async Task<Unit> Handle(DeleteAlbumCommand command, CancellationToken cancellationToken)
    {
        await _albumRepository.DeleteAsync(command.AlbumId);
        return Unit.Value;
    }
}