using LanguageExt;
using LanguageExt.Common;
using MediatR;
using TL.Domain.ValueObjects.ArtistValueObjects;
using TL.Repository;
using static LanguageExt.Prelude;
using Unit = LanguageExt.Unit;

namespace TL.CQRS.ArtistCQ.Commands;

public class UpdateArtistCommand : IRequest<Option<Validation<Error, Unit>>>
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
    IRequestHandler<UpdateArtistCommand, Option<Validation<Error, Unit>>>
{
    private readonly IArtistRepository _artistRepository;

    public UpdateArtistCommandHandler(IArtistRepository artistRepository)
    {
        _artistRepository = artistRepository;
    }

    public async Task<Option<Validation<Error, Unit>>> Handle
        (UpdateArtistCommand command, CancellationToken cancellationToken)
    {
        var artist = await _artistRepository.FindAsync(command.Id);
        var name = ArtistName.Create(command.Name);
        var updatedArtist = artist
            .Map(a => name.Map(a.Update));

        ignore(updatedArtist
            .Map(a =>
                a.Map(async x => await _artistRepository.SaveAsync())));

        return updatedArtist;
    }
}