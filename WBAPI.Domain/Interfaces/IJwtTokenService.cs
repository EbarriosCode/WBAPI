namespace WBAPI.Domain.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(string userId, string email, string username, IList<string> roles);
        DateTime GetExpiration();
    }
}
