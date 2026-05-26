using FluentValidation;

namespace WBAPI.Application.Features.Albums.Commands.CreateAlbum
{
    public class CreateAlbumValidator : AbstractValidator<CreateAlbumCommand>
    {
        public CreateAlbumValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre del álbum es requerido.")
                .MaximumLength(200).WithMessage("Máximo 200 caracteres.");

            RuleFor(x => x.Artist)
                .NotEmpty().WithMessage("El artista es requerido.")
                .MaximumLength(150).WithMessage("Máximo 150 caracteres.");

            RuleFor(x => x.GenreId)
                .InclusiveBetween(1, 99).WithMessage("Género inválido.")
                .Must(id => Enum.IsDefined(typeof(Domain.Enums.Genre), id))
                .WithMessage("El género especificado no existe.");

            RuleFor(x => x.Year)
                .InclusiveBetween(1900, DateTime.UtcNow.Year)
                .WithMessage($"El año debe estar entre 1900 y {DateTime.UtcNow.Year}.");
        }
    }
}
