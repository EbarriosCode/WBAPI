namespace WBAPI.Application.Features.Albums.DTOs
{
    public record CreateAlbumDto(
        string Name,
        string Artist,
        int GenreId,   // Client send 1 = Balada, 2 = Pop, etc....
        int Year
    );
}
