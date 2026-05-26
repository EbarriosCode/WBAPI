using MediatR;
using WBAPI.Application.Common;
using WBAPI.Application.Features.Albums.DTOs;

namespace WBAPI.Application.Features.Albums.Queries.GetAllAlbums
{
    public record GetAllAlbumsQuery() : IRequest<BaseResponse<IReadOnlyList<AlbumDto>>>;
}
