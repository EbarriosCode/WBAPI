using MediatR;
using WBAPI.Application.Common;
using WBAPI.Application.Features.Albums.DTOs;
using WBAPI.Application.Interfaces;
using WBAPI.Domain.Enums;
using WBAPI.Domain.Interfaces;

namespace WBAPI.Application.Features.Albums.Commands.UpdateAlbum
{
    public class UpdateAlbumHandler(IAlbumRepository albumRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateAlbumCommand, BaseResponse<AlbumDto>>
    {
        public async Task<BaseResponse<AlbumDto>> Handle(UpdateAlbumCommand request, CancellationToken cancellationToken)
        {
            var album = await albumRepository.GetByIdAsync(request.Id, cancellationToken);

            if (album is null)
                return new BaseResponse<AlbumDto>(false, "Álbum no encontrado.");

            album.Update(request.Name, request.Artist, (Genre)request.GenreId, request.Year);

            albumRepository.Update(album);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new BaseResponse<AlbumDto>(true, "Álbum actualizado correctamente.", album.ToDto());
        }
    }

}
