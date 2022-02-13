using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using MediatR;
using TL.Domain;
using TL.Domain.ValueObjects.ArtistValueObjects;
using TL.Repository;

namespace TL.Api.CQRS.ArtistCQ.Commands;

public class UpdateArtistCommand : IRequest<Option<Validation<Error, Artist>>>
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
    IRequestHandler<UpdateArtistCommand, Option<Validation<Error, Artist>>>
{
    private readonly IArtistRepository _artistRepository;

    public UpdateArtistCommandHandler(IArtistRepository artistRepository)
    {
        _artistRepository = artistRepository;
    }

    public async Task<Option<Validation<Error, Artist>>> Handle
        (UpdateArtistCommand command, CancellationToken cancellationToken)
    {
        var artist = await _artistRepository.FindAsync(command.Id);
        var name = ArtistName.Create(command.Name);
        var updatedArtist = artist
            .Map(a => (name)
                .Map(n => a.Update(n)));

        ignore(updatedArtist
            .Map(a =>
                a.Map(async x => await _artistRepository.UpdateAsync(x))));

        return updatedArtist;

    }
}