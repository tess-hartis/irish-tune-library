using LanguageExt;
using LanguageExt.Common;
using MediatR;
using TL.Api.DTOs.AlbumDTOs;
using TL.Api.DTOs.TuneDTOS;
using TL.Domain;
using TL.Domain.ValueObjects.AlbumValueObjects;
using TL.Repository;

namespace TL.Api.CQRS.AlbumCQ.Commands;

public class UpdateAlbumCommand : IRequest<Validation<Error, Album>>
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

public class UpdateAlbumCommandHandler : IRequestHandler<UpdateAlbumCommand, Validation<Error, Album>>
{
    private readonly IAlbumRepository _albumRepository;

    public UpdateAlbumCommandHandler(IAlbumRepository albumRepository)
    {
        _albumRepository = albumRepository;
    }

    public async Task<Validation<Error, Album>> Handle(UpdateAlbumCommand command, CancellationToken cancellationToken)
    {
        var album = await _albumRepository.FindAsync(command.AlbumId);
        var title = AlbumTitle.Create(command.Title);
        var year = AlbumYear.Create(command.Year);

        var updatedAlbum = (title, year)
            .Apply((t, y) => album.Update(t, y));

        await updatedAlbum
            .Succ(async a => { await _albumRepository.UpdateAsync(album.Id); })
            .Fail(e => e.AsTask());

        return updatedAlbum;
    }
}