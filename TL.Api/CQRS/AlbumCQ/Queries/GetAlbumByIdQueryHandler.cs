using MediatR;
using TL.Api.DTOs.AlbumDTOs;
using TL.Domain;
using TL.Repository;

namespace TL.Api.CQRS.AlbumCQ.Queries;

public class GetAlbumByIdQuery : IRequest<Album>
{
    public int Id { get; }

    public GetAlbumByIdQuery(int id)
    {
        Id = id;
    }
}

public class GetAlbumByIdQueryHandler : IRequestHandler<GetAlbumByIdQuery, Album>
{
    private readonly IAlbumRepository _albumRepository;

    public GetAlbumByIdQueryHandler(IAlbumRepository albumRepository)
    {
        _albumRepository = albumRepository;
    }

    public async Task<Album> Handle
        (GetAlbumByIdQuery request, CancellationToken cancellationToken)
    {
        var album = await _albumRepository.FindAsync(request.Id);
        return album;

    }
}