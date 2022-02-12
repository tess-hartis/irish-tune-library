using LanguageExt;
using LanguageExt.Common;
using MediatR;
using TL.Api.DTOs.AlbumDTOs;
using TL.Domain;
using TL.Domain.ValueObjects.AlbumValueObjects;
using TL.Repository;

namespace TL.Api.CQRS.AlbumCQ.Commands;

public class CreateAlbumCommand : IRequest<Validation<Error, Album>>
{
    public string Title { get; }
    public int Year { get; }

    public CreateAlbumCommand(string title, int year)
    {
        Title = title;
        Year = year;
    }
}
public class CreateAlbumCommandHandler : IRequestHandler<CreateAlbumCommand, Validation<Error, Album>>
{
    private readonly IAlbumRepository _albumRepository;

    public CreateAlbumCommandHandler(IAlbumRepository albumRepository)
    {
        _albumRepository = albumRepository;
    }

    public async Task<Validation<Error, Album>> Handle(CreateAlbumCommand command, CancellationToken cancellationToken)
    {
        var title = AlbumTitle.Create(command.Title);
        var year = AlbumYear.Create(command.Year);

        var album = (title, year)
            .Apply((t, y) => Album.Create(t, y));

        await album
            .Succ(async a =>
            {
                await _albumRepository.AddAsync(a);
            })
            .Fail(e => e.AsTask());

        return album;
    }
}