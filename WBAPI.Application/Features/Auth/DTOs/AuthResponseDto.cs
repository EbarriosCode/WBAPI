namespace WBAPI.Application.Features.Auth.DTOs
{
    public record AuthResponseDto(string UserId, string Username, string Email, string Role, string Token, DateTime ExpiresAt);
}
