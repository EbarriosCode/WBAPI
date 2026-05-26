namespace WBAPI.Application.Features.Albums.DTOs
{
    public record UpdateAlbumDto(
        string Name,
        string Artist,
        int GenreId,
        int Year
    );
}
