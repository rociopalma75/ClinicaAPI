
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Clinica_TFI.Application.DTO
{
    public record RecetaDigitalRequestDTO
    {
        public string Observaciones { get; set; }
        public string Medicamentos { get; set; }
    }

    public record PedidoLaboratorioRequestDTO
    {
        public string Descripcion { get; set; }
    }

    public record RecetaDigitalExample : IExamplesProvider<RecetaDigitalRequestDTO>
    {
        public RecetaDigitalRequestDTO GetExamples()
        {
            return new RecetaDigitalRequestDTO
            {
                Observaciones = "Tomar el medicamento cada 8 hs",
                Medicamentos = "11111,12345"
            };
        }
    }
}
