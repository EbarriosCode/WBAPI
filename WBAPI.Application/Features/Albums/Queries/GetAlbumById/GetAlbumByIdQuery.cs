using MediatR;
using WBAPI.Application.Common;
using WBAPI.Application.Features.Albums.DTOs;

namespace WBAPI.Application.Features.Albums.Queries.GetAlbumById
{
    public record GetAlbumByIdQuery(Guid Id) : IRequest<BaseResponse<AlbumDto>>;
}
