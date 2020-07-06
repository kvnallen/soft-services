using FluentValidation;
using Juros.Constants;
using Swashbuckle.AspNetCore.Annotations;

namespace Juros.DTO
{
    public class CalculoCommand
    {
        [SwaggerParameter(Required = true)]
        public double ValorInicial { get; set; }

        [SwaggerParameter(Required = true)]
        public int Meses { get; set; }
    }

    public class CalculoCommandValidator : AbstractValidator<CalculoCommand>
    {
        public CalculoCommandValidator()
        {
            RuleFor(x => x.Meses).GreaterThan(0).WithMessage(ValidationErrors.MaiorOuIgualZero);
            RuleFor(x => x.ValorInicial).GreaterThan(0).WithMessage(ValidationErrors.MaiorOuIgualZero);
        }
    }
}