using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using MediatR;
using TL.Domain;
using TL.Domain.ValueObjects.AlbumValueObjects;
using TL.Repository;

namespace TL.Api.CQRS.AlbumCQ.Commands;

public class UpdateAlbumCommand : IRequest<Option<Validation<Error, Album>>>
{
    public int AlbumId { get; set; }
    public string Title { get; }
    public int Year { get; }

    public UpdateAlbumCommand(int albumId, string title, int year)
    {
        AlbumId = albumId;
        Title = title;
        Year = year;
    }
}

public class UpdateAlbumCommandHandler : IRequestHandler<UpdateAlbumCommand, Option<Validation<Error, Album>>>
{
    private readonly IAlbumRepository _albumRepository;

    public UpdateAlbumCommandHandler(IAlbumRepository albumRepository)
    {
        _albumRepository = albumRepository;
    }

    public async Task<Option<Validation<Error, Album>>> Handle
        (UpdateAlbumCommand command, CancellationToken cancellationToken)
    {
        var album = await _albumRepository.FindAsync(command.AlbumId);
        var title = AlbumTitle.Create(command.Title);
        var year = AlbumYear.Create(command.Year);

        var updatedAlbum = album
            .Map(a => (title, year)
                .Apply((t, y) => a.Update(t, y)));

        ignore(updatedAlbum
            .Map(a =>
                a.Map(async x => await _albumRepository.UpdateAsync(x))));

        return updatedAlbum;
    }
}