using MediatR;
using TL.Repository;

namespace TL.Api.CQRS.ArtistCQ.Commands;

public class DeleteArtistCommand : IRequest<Unit>
{
    public int Id { get; }

    public DeleteArtistCommand(int id)
    {
        Id = id;
    }
}
public class DeleteArtistCommandHandler : IRequestHandler<DeleteArtistCommand, Unit>
{
    private readonly IArtistRepository _artistRepository;

    public DeleteArtistCommandHandler(IArtistRepository artistRepository)
    {
        _artistRepository = artistRepository;
    }

    public async Task<Unit> Handle(DeleteArtistCommand command, CancellationToken cancellationToken)
    {
        await _artistRepository.DeleteAsync(command.Id);
        return Unit.Value;
    }
}