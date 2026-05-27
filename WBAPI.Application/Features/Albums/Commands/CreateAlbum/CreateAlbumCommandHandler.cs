using MediatR;
using WBAPI.Application.Common;
using WBAPI.Application.Features.Albums.DTOs;
using WBAPI.Application.Interfaces;
using WBAPI.Domain.Entities;
using WBAPI.Domain.Enums;
using WBAPI.Domain.Interfaces;

namespace WBAPI.Application.Features.Albums.Commands.CreateAlbum
{
    public class CreateAlbumCommandHandler(IAlbumRepository albumRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateAlbumCommand, BaseResponse<AlbumDto>>
    {
        public async Task<BaseResponse<AlbumDto>> Handle(
            CreateAlbumCommand request, CancellationToken cancellationToken)
        {
            var genre = (Genre)request.GenreId;
            var album = Album.Create(request.Name, request.Artist, genre, request.Year);

            await albumRepository.AddAsync(album, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new BaseResponse<AlbumDto>(true, "Álbum creado exitosamente.", album.ToDto());
        }
    }
}
