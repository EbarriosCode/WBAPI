using WBAPI.Application.Features.Auth.DTOs;
using WBAPI.Domain.Common;

namespace WBAPI.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<DomainResult<AuthResponseDto>> RegisterAsync(string username, string email, string password, CancellationToken cancellationToken = default);
        Task<DomainResult<AuthResponseDto>> LoginAsync(string email, string password, CancellationToken cancellationToken = default);        
    }
}
