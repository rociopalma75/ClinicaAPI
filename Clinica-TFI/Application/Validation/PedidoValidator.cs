using Clinica_TFI.Application.DTO;

namespace Clinica_TFI.Application.Validation
{
    public class PedidoValidator : BaseValidator<PedidoLaboratorioRequestDTO>
    {
        public PedidoValidator()
        {
            ValidateString(p => p.Descripcion, 5, 100);
        }
    }
}
