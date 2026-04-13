namespace Clinica_TFI.Application.DTO
{
    public record MedicoResponseDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string MatriculaMedica { get; set; } =null!;
        public string Especialidad { get; set; } = null!;
        public string Correo { get; set; } = null!;
    }

    public record MedicoRequestDTO
    {
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string MatriculaMedica { get; set; } = null!;
        public string Especialidad { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Clave {  get; set; } = null!;
    }

    public record MedicoLogInDTO
    {
        public string Correo { get; set; } = null!;
        public string Clave { get; set; } = null!;
    }
}
