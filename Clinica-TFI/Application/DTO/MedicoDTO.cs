namespace Clinica_TFI.Application.DTO
{
    public record MedicoResponseDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string MatriculaMedica { get; set; }
        public string Especialidad { get; set; }
        public string Correo { get; set; }
    }

    public record MedicoRequestDTO
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string MatriculaMedica {  get; set; }
        public string Especialidad { get; set; }
        public string Correo { get; set; }

        public string Clave {  get; set; }
    }

    public record MedicoLogInDTO
    {
        public string Correo { get; set; }
        public string Clave { get; set; }
    }
}
