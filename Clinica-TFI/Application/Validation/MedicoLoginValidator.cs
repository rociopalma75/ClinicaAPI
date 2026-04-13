using Clinica_TFI.Application.DTO;
using FluentValidation;

namespace Clinica_TFI.Application.Validation
{
    public class MedicoLoginValidator : BaseValidator<MedicoLogInDTO>
    {
        public MedicoLoginValidator()
        {
            ValidateEmail(p => p.Correo);
            RuleFor(m => m.Clave)
                .NotEmpty()
                .MinimumLength(4);
                
        }
    }
}
