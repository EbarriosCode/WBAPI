using MediatR;
using WBAPI.Application.Common;
using WBAPI.Application.Features.Auth.DTOs;
using WBAPI.Application.Interfaces;

namespace WBAPI.Application.Features.Auth.Commands.RegisterUser
{
    public class RegisterUserCommandHandler(IIdentityService identityService) : IRequestHandler<RegisterUserCommand, BaseResponse<AuthResponseDto>>
    {
        public async Task<BaseResponse<AuthResponseDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var result = await identityService.RegisterAsync(request.Username, request.Email, request.Password, cancellationToken);

            if (!result.IsSuccess)
                return new BaseResponse<AuthResponseDto>(false, string.Join(", ", result.Errors));

            return new BaseResponse<AuthResponseDto>(true, "Usuario registrado exitosamente.", result.Value);
        }
    }
}
