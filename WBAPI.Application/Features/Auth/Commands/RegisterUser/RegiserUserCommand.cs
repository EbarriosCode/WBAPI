using MediatR;
using WBAPI.Application.Common;
using WBAPI.Application.Features.Auth.DTOs;

namespace WBAPI.Application.Features.Auth.Commands.RegisterUser
{
    public record RegisterUserCommand(string Username, string Email, string Password, string ConfirmPassword) : IRequest<BaseResponse<AuthResponseDto>>;
}
