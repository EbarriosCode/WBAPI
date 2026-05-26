using MediatR;
using WBAPI.Application.Common;
using WBAPI.Application.Features.Albums.DTOs;

namespace WBAPI.Application.Features.Albums.Commands.UpdateAlbum
{
    public record UpdateAlbumCommand(Guid Id, string Name, string Artist, int GenreId, int Year) : IRequest<BaseResponse<AlbumDto>>;
}
