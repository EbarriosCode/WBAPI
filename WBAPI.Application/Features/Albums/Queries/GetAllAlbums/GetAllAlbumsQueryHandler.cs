using MediatR;
using WBAPI.Application.Common;
using WBAPI.Application.Features.Albums.DTOs;
using WBAPI.Domain.Interfaces;

namespace WBAPI.Application.Features.Albums.Queries.GetAllAlbums
{
    public class GetAllAlbumsQueryHandler(IAlbumRepository albumRepository) : IRequestHandler<GetAllAlbumsQuery, BaseResponse<IReadOnlyList<AlbumDto>>>
    {
        public async Task<BaseResponse<IReadOnlyList<AlbumDto>>> Handle(GetAllAlbumsQuery request, CancellationToken cancellationToken)
        {
            var albums = await albumRepository.GetAllAsync(cancellationToken);
            var dtos = albums.Select(a => a.ToDto()).ToList();

            return new BaseResponse<IReadOnlyList<AlbumDto>>(true, $"{dtos.Count} álbum(es) encontrado(s).", dtos);
        }
    }
}
