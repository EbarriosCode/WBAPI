using MediatR;
using WBAPI.Application.Common;
using WBAPI.Application.Interfaces;
using WBAPI.Domain.Interfaces;

namespace WBAPI.Application.Features.Albums.Commands.DeleteAlbum
{
    public class DeleteAlbumCommandHandler(IAlbumRepository albumRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteAlbumCommand, BaseResponse<string>>
    {
        public async Task<BaseResponse<string>> Handle(DeleteAlbumCommand request, CancellationToken ct)
        {
            var album = await albumRepository.GetByIdAsync(request.Id, ct);

            if (album is null)
                return new BaseResponse<string>(false, "Álbum no encontrado.");

            album.Delete();          // soft-delete: IsActive = false
            albumRepository.Update(album);
            await unitOfWork.SaveChangesAsync(ct);

            return new BaseResponse<string>(true, $"Álbum '{album.Name}' eliminado correctamente.");
        }
    }

}
