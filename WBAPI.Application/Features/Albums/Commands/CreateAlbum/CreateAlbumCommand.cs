using MediatR;
using WBAPI.Application.Common;
using WBAPI.Application.Features.Albums.DTOs;

namespace WBAPI.Application.Features.Albums.Commands.CreateAlbum
{
    public record CreateAlbumCommand(string Name, string Artist, int GenreId, int Year) : IRequest<BaseResponse<AlbumDto>>;
}
