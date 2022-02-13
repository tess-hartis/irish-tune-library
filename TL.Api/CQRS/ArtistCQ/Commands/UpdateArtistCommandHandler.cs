using LanguageExt;
using LanguageExt.Common;
using MediatR;
using TL.Domain;
using TL.Domain.ValueObjects.ArtistValueObjects;
using TL.Repository;

namespace TL.Api.CQRS.ArtistCQ.Commands;

public class UpdateArtistCommand : IRequest<Validation<Error, Artist>>
{
    public int Id { get; set; }
    public string Name { get; }

    public UpdateArtistCommand(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
public class UpdateArtistCommandHandler : 
    IRequestHandler<UpdateArtistCommand, Validation<Error, Artist>>
{
    private readonly IArtistRepository _artistRepository;

    public UpdateArtistCommandHandler(IArtistRepository artistRepository)
    {
        _artistRepository = artistRepository;
    }

    public async Task<Validation<Error, Artist>> Handle
        (UpdateArtistCommand command, CancellationToken cancellationToken)
    {
        var artist = await _artistRepository.FindAsync(command.Id);
        var name = ArtistName.Create(command.Name);
        var updatedArtist = name.Map(n => artist.Update(n));

        await updatedArtist
            .Succ(async a => { await _artistRepository.UpdateAsync(artist.Id); })
            .Fail(e => e.AsTask());

        return updatedArtist;
    }
}