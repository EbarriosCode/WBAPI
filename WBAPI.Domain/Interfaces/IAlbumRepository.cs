using WBAPI.Domain.Entities;

namespace WBAPI.Domain.Interfaces
{
    public interface IAlbumRepository
    {
        Task<IReadOnlyList<Album>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Album?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
        Task AddAsync(Album album, CancellationToken cancellationToken = default);
        void Update(Album album);
        void Remove(Album album);   // o soft-delete via album.Delete()
    }
}
