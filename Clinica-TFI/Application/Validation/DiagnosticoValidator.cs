using Clinica_TFI.Application.DTO;

namespace Clinica_TFI.Application.Validation
{
    public class DiagnosticoValidator : BaseValidator<DiagnosticoRequestDTO>
    {
        public DiagnosticoValidator()
        {
            ValidateString(d => d.Descripcion, 5, 200);
        }
    }
}
