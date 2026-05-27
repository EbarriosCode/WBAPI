using MediatR;
using WBAPI.Application.Common;
using WBAPI.Application.Features.Albums.DTOs;
using WBAPI.Domain.Interfaces;

namespace WBAPI.Application.Features.Albums.Queries.GetAlbumById
{
    public class GetAlbumByIdQueryHandler(IAlbumRepository albumRepository) : IRequestHandler<GetAlbumByIdQuery, BaseResponse<AlbumDto>>
    {
        public async Task<BaseResponse<AlbumDto>> Handle(GetAlbumByIdQuery request, CancellationToken ct)
        {
            var album = await albumRepository.GetByIdAsync(request.Id, ct);

            return album is null
                                ? new BaseResponse<AlbumDto>(false, "Álbum no encontrado.")
                                : new BaseResponse<AlbumDto>(true, "Álbum encontrado.", album.ToDto());
        }
    }
}
