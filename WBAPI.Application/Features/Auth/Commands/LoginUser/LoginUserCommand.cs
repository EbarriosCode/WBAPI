using MediatR;
using WBAPI.Application.Common;
using WBAPI.Application.Features.Auth.DTOs;

namespace WBAPI.Application.Features.Auth.Commands.LoginUser
{
    public record LoginUserCommand(string Email, string Password) : IRequest<BaseResponse<AuthResponseDto>>;

}
