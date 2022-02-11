using MediatR;
using TL.Api.DTOs.AlbumDTOs;
using TL.Repository;

namespace TL.Api.CQRS.AlbumCQ.Queries;

public class GetAlbumByIdQuery : IRequest<GetAlbumDTO>
{
    public int Id { get; }

    public GetAlbumByIdQuery(int id)
    {
        Id = id;
    }
}

public class GetAlbumByIdQueryHandler : IRequestHandler<GetAlbumByIdQuery, GetAlbumDTO>
{
    private readonly IAlbumRepository _albumRepository;

    public GetAlbumByIdQueryHandler(IAlbumRepository albumRepository)
    {
        _albumRepository = albumRepository;
    }

    public async Task<GetAlbumDTO> Handle
        (GetAlbumByIdQuery request, CancellationToken cancellationToken)
    {
        var album = await _albumRepository.FindAsync(request.Id);
        return GetAlbumDTO.FromAlbum(album);
        
    }
}