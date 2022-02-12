using MediatR;
using TL.Domain;
using TL.Domain.ValueObjects.ArtistValueObjects;
using TL.Repository;

namespace TL.Api.CQRS.ArtistCQ.Commands;

public class UpdateArtistCommand : IRequest<Artist>
{
    public int Id { get; }
    public ArtistName Name { get; }

    public UpdateArtistCommand(int id, ArtistName name)
    {
        Id = id;
        Name = name;
    }
}
public class UpdateArtistCommandHandler : IRequestHandler<UpdateArtistCommand, Artist>
{
    private readonly IArtistRepository _artistRepository;

    public UpdateArtistCommandHandler(IArtistRepository artistRepository)
    {
        _artistRepository = artistRepository;
    }

    public async Task<Artist> Handle(UpdateArtistCommand command, CancellationToken cancellationToken)
    {
        var artist = await _artistRepository.UpdateArtist(command.Id, command.Name);
        return artist;
    }
}