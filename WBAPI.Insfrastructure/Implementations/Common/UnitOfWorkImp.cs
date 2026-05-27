using WBAPI.Application.Interfaces;
using WBAPI.Insfrastructure.Implementations.Persistence;

namespace WBAPI.Infrastructure.Implementations.Common
{
    public class UnitOfWorkImp(AppDbContext context) : IUnitOfWork
    {
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => context.SaveChangesAsync(cancellationToken);
    }
}
