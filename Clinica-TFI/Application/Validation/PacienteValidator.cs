using Clinica_TFI.Application.DTO;
using FluentValidation;

namespace Clinica_TFI.Application.Validation
{
    public class PacienteValidator : BaseValidator<PacienteRequestDTO>
    {
        public PacienteValidator()
        {
            ValidateString(p => p.Nombre, 3, 50);
            ValidateString(p => p.Apellido, 3, 50);
            ValidateDniCuil(p => p.Dni, 8);
            ValidateDniCuil(p => p.Cuil, 11);

            RuleFor(p => p.FechaNacimiento)
                .NotEmpty()
                .LessThan(DateOnly.FromDateTime(DateTime.Now));

            ValidateEmail(p => p.Email);

            RuleFor(p => p.Telefono)
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(15)
                .Must(campo => campo.All(c => char.IsDigit(c))).WithMessage("{PropertyName} solo debe contener números.");

            RuleFor(p => p.Domicilio)
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(150)
                .Must(campo => campo.All(c => char.IsLetter(c) || char.IsDigit(c) || char.IsWhiteSpace(c) || char.IsPunctuation(c)));
        }
    }

    
}
