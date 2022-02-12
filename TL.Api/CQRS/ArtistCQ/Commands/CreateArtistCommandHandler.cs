using MediatR;
using TL.Domain;
using TL.Domain.ValueObjects.ArtistValueObjects;
using TL.Repository;

namespace TL.Api.CQRS.ArtistCQ.Commands;

public class CreateArtistCommand : IRequest<Artist>
{
    public ArtistName Name { get; }

    public CreateArtistCommand(ArtistName name)
    {
        Name = name;
    }
}
public class CreateArtistCommandHandler : IRequestHandler<CreateArtistCommand, Artist>
{
    private readonly IArtistRepository _artistRepository;

    public CreateArtistCommandHandler(IArtistRepository artistRepository)
    {
        _artistRepository = artistRepository;
    }

    public async Task<Artist> Handle(CreateArtistCommand command, CancellationToken cancellationToken)
    {
        var artist = Artist.CreateArtist(command.Name);
        await _artistRepository.AddAsync(artist);
        return artist;
    }
}