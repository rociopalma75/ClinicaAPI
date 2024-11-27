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
}
