using WBAPI.Domain.Entities;

namespace WBAPI.Application.Features.Albums.DTOs
{
    public static class AlbumMappingExtensions
    {
        public static AlbumDto ToDto(this Album album) => new(
            album.Id,
            album.Name,
            album.Artist,
            album.Genre.ToString(),   // "Balada", "Pop"…
            (int)album.Genre,         // 1, 2…
            album.Year,
            album.CreatedAt,
            album.UpdatedAt
        );
    }

}
