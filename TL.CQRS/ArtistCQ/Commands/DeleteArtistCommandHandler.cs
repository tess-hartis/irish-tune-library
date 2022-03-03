using LanguageExt;
using MediatR;
using TL.Repository;
using static LanguageExt.Prelude;
using Unit = LanguageExt.Unit;


namespace TL.CQRS.ArtistCQ.Commands;

public class DeleteArtistCommand : IRequest<Option<Unit>>
{
    public int Id { get; }

    public DeleteArtistCommand(int id)
    {
        Id = id;
    }
}
public class DeleteArtistCommandHandler : IRequestHandler<DeleteArtistCommand, Option<Unit>>
{
    private readonly IArtistRepository _artistRepository;

    public DeleteArtistCommandHandler(IArtistRepository artistRepository)
    {
        _artistRepository = artistRepository;
    }

    public async Task<Option<Unit>> Handle(DeleteArtistCommand command, CancellationToken cancellationToken)
    {
        var artist = await _artistRepository.FindAsync(command.Id);
        ignore(artist.Map(async a => await _artistRepository.DeleteAsync(a)));
        return artist.Map(a => unit);
    }
}