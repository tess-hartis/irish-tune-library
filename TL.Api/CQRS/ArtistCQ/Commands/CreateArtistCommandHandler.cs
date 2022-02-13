using LanguageExt;
using LanguageExt.Common;
using MediatR;
using TL.Domain;
using TL.Domain.ValueObjects.ArtistValueObjects;
using TL.Repository;

namespace TL.Api.CQRS.ArtistCQ.Commands;

public class CreateArtistCommand : IRequest<Validation<Error, Artist>>
{
    public string Name { get; }

    public CreateArtistCommand(string name)
    {
        Name = name;
    }
}
public class CreateArtistCommandHandler : IRequestHandler<CreateArtistCommand, Validation<Error, Artist>>
{
    private readonly IArtistRepository _artistRepository;

    public CreateArtistCommandHandler(IArtistRepository artistRepository)
    {
        _artistRepository = artistRepository;
    }

    public async Task<Validation<Error, Artist>> Handle(CreateArtistCommand command, CancellationToken cancellationToken)
    {
        var name = ArtistName.Create(command.Name);
        var artist = name.Map(n => Artist.CreateArtist(n));
        await artist
            .Succ(async a =>
            {
                await _artistRepository.AddAsync(a);
            })
            .Fail(e => e.AsTask());

        return artist;
    }
}