using MediatR;
using TL.Api.DTOs.AlbumDTOs;
using TL.Api.DTOs.TuneDTOS;
using TL.Domain;
using TL.Domain.ValueObjects.AlbumValueObjects;
using TL.Repository;

namespace TL.Api.CQRS.AlbumCQ.Commands;

public class UpdateAlbumCommand : IRequest<Album>
{
    public int AlbumId { get; }
    public AlbumTitle Title { get; }
    public AlbumYear Year { get; }

    public UpdateAlbumCommand(int albumId, AlbumTitle title, AlbumYear year)
    {
        AlbumId = albumId;
        Title = title;
        Year = year;
    }
}

public class UpdateAlbumCommandHandler : IRequestHandler<UpdateAlbumCommand, Album>
{
    private readonly IAlbumRepository _albumRepository;

    public UpdateAlbumCommandHandler(IAlbumRepository albumRepository)
    {
        _albumRepository = albumRepository;
    }

    public async Task<Album> Handle(UpdateAlbumCommand command, CancellationToken cancellationToken)
    {
        var album = await _albumRepository.UpdateAlbum(command.AlbumId, command.Title, command.Year);
        return album;
    }
}