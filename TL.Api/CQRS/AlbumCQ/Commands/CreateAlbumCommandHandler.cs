using MediatR;
using TL.Api.DTOs.AlbumDTOs;
using TL.Domain;
using TL.Domain.ValueObjects.AlbumValueObjects;
using TL.Repository;

namespace TL.Api.CQRS.AlbumCQ.Commands;

public class CreateAlbumCommand : IRequest<Album>
{
    public AlbumTitle Title { get; }
    public AlbumYear Year { get; }

    public CreateAlbumCommand(AlbumTitle title, AlbumYear year)
    {
        Title = title;
        Year = year;
    }
}
public class CreateAlbumCommandHandler : IRequestHandler<CreateAlbumCommand, Album>
{
    private readonly IAlbumRepository _albumRepository;

    public CreateAlbumCommandHandler(IAlbumRepository albumRepository)
    {
        _albumRepository = albumRepository;
    }

    public async Task<Album> Handle(CreateAlbumCommand command, CancellationToken cancellationToken)
    {
        var album = Album.Create(command.Title, command.Year);
        await _albumRepository.AddAsync(album);
        return album;
    }
}