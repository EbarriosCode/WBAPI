using MediatR;
using WBAPI.Application.Common;
using WBAPI.Application.Features.Auth.DTOs;
using WBAPI.Application.Interfaces;

namespace WBAPI.Application.Features.Auth.Commands.LoginUser
{
    public class LoginUserCommandHandler(IIdentityService identityService) : IRequestHandler<LoginUserCommand, BaseResponse<AuthResponseDto>>
    {
        public async Task<BaseResponse<AuthResponseDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var result = await identityService.LoginAsync(request.Email, request.Password, cancellationToken);

            if (!result.IsSuccess)
                return new BaseResponse<AuthResponseDto>(false, string.Join(", ", result.Errors));

            return new BaseResponse<AuthResponseDto>(true, "Login exitoso.", result.Value);
        }
    }
}
