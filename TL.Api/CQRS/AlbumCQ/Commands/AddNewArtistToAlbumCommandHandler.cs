using LanguageExt;
using LanguageExt.Common;
using MediatR;
using TL.Api.DTOs.AlbumDTOs;
using TL.Domain;
using TL.Domain.ValueObjects.ArtistValueObjects;
using TL.Repository;

namespace TL.Api.CQRS.AlbumCQ.Commands;

public class AddNewArtistToAlbumCommand : IRequest<Validation<Error, Artist>>
{
    public int AlbumId { get; set; }
    public string Name { get; }

    public AddNewArtistToAlbumCommand(int albumId, string name)
    {
        AlbumId = albumId;
        Name = name;
    }
}
public class AddNewArtistToAlbumCommandHandler : IRequestHandler<AddNewArtistToAlbumCommand, Validation<Error, Artist>>
{
    private readonly IAlbumArtistService _albumArtistService;

    public AddNewArtistToAlbumCommandHandler(IAlbumArtistService albumArtistService)
    {
        _albumArtistService = albumArtistService;
    }

    public async Task<Validation<Error, Artist>> Handle(AddNewArtistToAlbumCommand command, CancellationToken cancellationToken)
    {
        var name = ArtistName.Create(command.Name);
        var artist = name.Map(n => Artist.CreateArtist(n));

        await artist
            .Succ(async a =>
            {
                await _albumArtistService.AddNewArtistToAlbum(command.AlbumId, a);
            })
            .Fail(e => e.AsTask());

        return artist;
    }
}