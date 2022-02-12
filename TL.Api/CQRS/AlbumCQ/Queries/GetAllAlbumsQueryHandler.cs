using MediatR;
using Microsoft.EntityFrameworkCore;
using TL.Api.DTOs.AlbumDTOs;
using TL.Domain;
using TL.Repository;

namespace TL.Api.CQRS.AlbumCQ.Queries;

public record GetAllAlbumsQuery : IRequest<IEnumerable<Album>>;

public class GetAllAlbumsQueryHandler : IRequestHandler<GetAllAlbumsQuery, IEnumerable<Album>>
{
    private readonly IAlbumRepository _albumRepository;

    public GetAllAlbumsQueryHandler(IAlbumRepository albumRepository)
    {
        _albumRepository = albumRepository;
    }

    public async Task<IEnumerable<Album>> Handle
        (GetAllAlbumsQuery request, CancellationToken cancellationToken)
    {
        var albums = await _albumRepository.GetEntities().ToListAsync();
        return albums;

    }
}