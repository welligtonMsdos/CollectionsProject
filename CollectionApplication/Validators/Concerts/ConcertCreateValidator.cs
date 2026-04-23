using CollectionApplication.Dtos;
using FluentValidation;

namespace CollectionApplication.Validators.Concerts;

public class ConcertCreateValidator : AbstractValidator<ConcertCreateDto>
{
    public ConcertCreateValidator()
    {
        RuleFor(x => x.basedto.Artist)
          .NotEmpty().WithMessage("Artist is required")
          .MinimumLength(3).WithMessage("Artist must be at least 3 characters")
          .MaximumLength(50).WithMessage("Artist must not exceed 50 characters");

        RuleFor(x => x.basedto.Venue)
            .NotEmpty().WithMessage("Venue is required")
            .MinimumLength(3).WithMessage("Venue must be at least 3 characters")
            .MaximumLength(100).WithMessage("Venue must not exceed 30 characters");

        RuleFor(x => x.basedto.ShowDate)
            .NotEmpty()
            .WithMessage("Date is required.")
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-10)))
            .WithMessage("The show date cannot be older than 10 years ago.")
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow.AddYears(2)))
            .WithMessage("The show date cannot be more than 2 years in the future.");

        RuleFor(x => x.basedto.Photo)
            .NotEmpty().WithMessage("Photo is required")
            .MinimumLength(10).WithMessage("Photo URL must be at least 10 characters")
            .MaximumLength(255).WithMessage("Photo URL must not exceed 255 characters");
    }
}
