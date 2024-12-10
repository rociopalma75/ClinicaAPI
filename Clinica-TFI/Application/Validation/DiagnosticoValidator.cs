using Clinica_TFI.Application.DTO;
using FluentValidation;

namespace Clinica_TFI.Application.Validation
{
    public class DiagnosticoValidator : BaseValidator<DiagnosticoRequestDTO>
    {
        public DiagnosticoValidator()
        {
            RuleFor(d => d.Descripcion)
                .NotEmpty()
                .MinimumLength(4)
                .MaximumLength(200)
                .Must(diag => diag.All(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || c == ',' || c == '.'));
        }
    }
}
