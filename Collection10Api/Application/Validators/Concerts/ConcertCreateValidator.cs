using Collection10Api.Application.Dtos;
using FluentValidation;

namespace Collection10Api.Application.Validators.Concerts;

public class ConcertCreateValidator : AbstractValidator<ConcertCreateDto>
{
    public ConcertCreateValidator()
    {
        RuleFor(x => x.Artist)
          .NotEmpty().WithMessage("Artista é obrigatório")
          .MinimumLength(3).WithMessage("Artista deve ter pelo menos 3 caracteres")
          .MaximumLength(50).WithMessage("Artista não deve exceder 50 caracteres");

        RuleFor(x => x.Venue)
            .NotEmpty().WithMessage("Local é obrigatório")
            .MinimumLength(3).WithMessage("Local deve ter pelo menos 3 caracteres")
            .MaximumLength(100).WithMessage("Local não deve exceder 100 caracteres");

        RuleFor(x => x.ShowDate)
            .NotEmpty()
            .WithMessage("Data é obrigatória.")
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-10)))
            .WithMessage("A data do show não pode ser mais antiga que 10 anos.")
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow.AddYears(2)))
            .WithMessage("A data do show não pode ser mais de 2 anos no futuro.");

        RuleFor(x => x.Photo)
            .NotEmpty().WithMessage("Foto é obrigatória")
            .MinimumLength(10).WithMessage("URL da foto deve ter pelo menos 10 caracteres")
            .MaximumLength(255).WithMessage("URL da foto não deve exceder 255 caracteres");
    }
}
