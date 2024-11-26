using FluentValidation;
using System.Linq.Expressions;

namespace Clinica_TFI.Application.Validation
{
    public class BaseValidator<TRequest> : AbstractValidator<TRequest> where TRequest : class
    {
        protected void ValidateString(Expression<Func<TRequest, string>> expression, int longMin, int longMax)
        {
            RuleFor(expression)
                .NotEmpty()
                .MinimumLength(longMin).MaximumLength(longMax)
                .Must(campo => campo.All(c => char.IsLetter(c) || c == ',' || c == '.' || char.IsWhiteSpace(c)));
        }


        protected void ValidateDniCuil(Expression<Func<TRequest, string>> expression, int longitud)
        {
            RuleFor(expression)
                .NotEmpty()
                .Length(longitud)
                .Must(campo => campo.All(c => char.IsDigit(c))).WithMessage("{PropertyName} solo debe contener numeros.");
        }

        protected void ValidateEmail(Expression<Func<TRequest, string>> expression)
        {
            RuleFor(expression)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
