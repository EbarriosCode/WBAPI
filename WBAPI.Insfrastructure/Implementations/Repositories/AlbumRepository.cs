using Microsoft.EntityFrameworkCore;
using WBAPI.Domain.Entities;
using WBAPI.Domain.Interfaces;
using WBAPI.Insfrastructure.Implementations.Persistence;

namespace WBAPI.Infrastructure.Implementations.Repositories
{
    public class AlbumRepository(AppDbContext context) : IAlbumRepository
    {
        // GetAll only returns active items (HasQueryFilter already filters by IsActive = true)
        public async Task<IReadOnlyList<Album>> GetAllAsync(
            CancellationToken cancellationToken = default)
            => await context.Albums
                   .AsNoTracking()
                   .OrderBy(a => a.Artist).ThenBy(a => a.Year)
                   .ToListAsync(cancellationToken);

        public async Task<Album?> GetByIdAsync(
            Guid id, CancellationToken cancellationToken = default)
            => await context.Albums
                   .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        public async Task<bool> ExistsAsync(
            Guid id, CancellationToken cancellationToken = default)
            => await context.Albums.AnyAsync(a => a.Id == id, cancellationToken);

        public async Task AddAsync(
            Album album, CancellationToken cancellationToken = default)
            => await context.Albums.AddAsync(album, cancellationToken);

        public void Update(Album album)
            => context.Albums.Update(album);

        // Hard-delete physical (if we need moreover of the soft-delete)
        public void Remove(Album album)
            => context.Albums.Remove(album);
    }

}
