namespace Clinica_TFI.Application.DTO
{
    public record PacienteRequestDTO
    {
        public string Dni { get; set; }
        public string Cuil { get; set; }
        public DateOnly FechaNacimiento { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Domicilio { get; set; }
    }
}
