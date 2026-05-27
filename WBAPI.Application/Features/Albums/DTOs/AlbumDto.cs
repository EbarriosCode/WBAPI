namespace WBAPI.Application.Features.Albums.DTOs
{
    public record AlbumDto(
        Guid Id,
        string Name,
        string Artist,
        string Genre,
        int GenreId,
        int Year,
        DateTime CreatedAt,
        DateTime? UpdatedAt
    );
}
