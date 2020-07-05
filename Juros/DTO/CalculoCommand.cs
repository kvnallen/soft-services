using FluentValidation;
using Juros.Constants;

namespace Juros.DTO
{
    public class CalculoCommand
    {
        public double ValorInicial { get; set; }

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