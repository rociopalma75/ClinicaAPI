using Clinica_TFI.Application.DTO;

namespace Clinica_TFI.Application.Validation
{
    public class MedicoValidator : BaseValidator<MedicoRequestDTO>
    {
        public MedicoValidator()
        {
            ValidateString(m => m.Nombre, 3, 100);
            ValidateString(m => m.Apellido, 3, 100);
            ValidateString(m => m.Especialidad, 5, 100);
            ValidateEmail(m => m.Correo);
        }
    }
}
