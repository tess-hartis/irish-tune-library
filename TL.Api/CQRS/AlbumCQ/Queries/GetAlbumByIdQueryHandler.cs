using LanguageExt;
using MediatR;
using TL.Api.DTOs.AlbumDTOs;
using TL.Domain;
using TL.Repository;

namespace TL.Api.CQRS.AlbumCQ.Queries;

public class GetAlbumByIdQuery : IRequest<Option<Album>>
{
    public int Id { get; }

    public GetAlbumByIdQuery(int id)
    {
        Id = id;
    }
}

public class GetAlbumByIdQueryHandler : IRequestHandler<GetAlbumByIdQuery, Option<Album>>
{
    private readonly IAlbumRepository _albumRepository;

    public GetAlbumByIdQueryHandler(IAlbumRepository albumRepository)
    {
        _albumRepository = albumRepository;
    }

    public async Task<Option<Album>> Handle
        (GetAlbumByIdQuery request, CancellationToken cancellationToken)
    {
        return await _albumRepository.FindAsync(request.Id);
    }
}