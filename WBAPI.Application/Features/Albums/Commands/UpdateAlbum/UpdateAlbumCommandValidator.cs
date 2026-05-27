using FluentValidation;

namespace WBAPI.Application.Features.Albums.Commands.UpdateAlbum
{
    public class UpdateAlbumCommandValidator : AbstractValidator<UpdateAlbumCommand>
    {
        public UpdateAlbumCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("El Id es requerido.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre del álbum es requerido.")
                .MaximumLength(200).WithMessage("Máximo 200 caracteres.");

            RuleFor(x => x.Artist)
                .NotEmpty().WithMessage("El artista es requerido.")
                .MaximumLength(150).WithMessage("Máximo 150 caracteres.");

            RuleFor(x => x.GenreId)
                .Must(id => Enum.IsDefined(typeof(Domain.Enums.Genre), id))
                .WithMessage("El género especificado no existe.");

            RuleFor(x => x.Year)
                .InclusiveBetween(1900, DateTime.UtcNow.Year)
                .WithMessage($"Año inválido.");
        }
    }
}
