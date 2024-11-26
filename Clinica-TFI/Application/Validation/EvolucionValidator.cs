using Clinica_TFI.Application.DTO;
using FluentValidation;

namespace Clinica_TFI.Application.Validation
{
    public class EvolucionValidator : BaseValidator<EvolucionRequestDTO>
    {
        public EvolucionValidator()
        {
            RuleFor(e => e.Informe)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(300);
        }

    }
}
