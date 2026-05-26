using FluentValidation;

namespace WBAPI.Application.Features.Auth.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("El nombre de usuario es requerido.")
                .MinimumLength(3).WithMessage("Mínimo 3 caracteres.")
                .MaximumLength(50).WithMessage("Máximo 50 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es requerido.")
                .EmailAddress().WithMessage("Formato de email inválido.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es requerida.")
                .MinimumLength(8).WithMessage("Mínimo 8 caracteres.")
                .Matches("[A-Z]").WithMessage("Debe contener al menos una mayúscula.")
                .Matches("[0-9]").WithMessage("Debe contener al menos un número.");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Las contraseñas no coinciden.");
        }

    }
}
