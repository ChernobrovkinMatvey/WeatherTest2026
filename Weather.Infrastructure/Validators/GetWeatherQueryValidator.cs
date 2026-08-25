using FluentValidation;
using Weather.Application.Queries;

namespace Weather.Infrastructure.Validators;

public class GetWeatherQueryValidator : AbstractValidator<GetWeatherQuery>
{
    public GetWeatherQueryValidator()
    {
        RuleFor(x => x.Days)
            .Must(d => d is null or 3)
            .WithMessage("Days must be null or 3.");
    }
}