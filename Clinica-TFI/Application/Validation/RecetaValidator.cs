using Clinica_TFI.Application.DTO;
using FluentValidation;

namespace Clinica_TFI.Application.Validation
{
    public class RecetaValidator : BaseValidator<RecetaDigitalRequestDTO>
    {
        public RecetaValidator()
        {
            RuleFor(m => m.Medicamentos)
                .NotEmpty()
                .Must(med => med.All(c => char.IsDigit(c) || char.IsWhiteSpace(c) || c == ',')).WithMessage("El formato de medicamentos no corresponde. Se deben ingresar los códigos separados por ','");
        }
    }
}
