namespace WBAPI.Application.Features.Auth.DTOs
{
    public record RegisterRequestDto(string Username, string Email, string Password, string ConfirmPassword);
}
