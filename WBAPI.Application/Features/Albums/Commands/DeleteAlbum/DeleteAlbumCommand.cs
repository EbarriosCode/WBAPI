using MediatR;
using WBAPI.Application.Common;

namespace WBAPI.Application.Features.Albums.Commands.DeleteAlbum
{
    public record DeleteAlbumCommand(Guid Id) : IRequest<BaseResponse<string>>;
}
