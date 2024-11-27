using System.Text.Json.Serialization;

namespace Clinica_TFI.Domain
{
    public class Medico
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string MatriculaMedica { get; set; } = null!;
        public string Especialidad { get; set; } = null!;
        public string Correo { get; set; } = null!;
        [JsonIgnore]
        public string ClaveHash { get; set; } = null!;

        public Medico()
        {
            
        }

        public Medico(string nombre, string apellido, string matriculaMedica, string especialidad, string correo, string clave)
        {
            Nombre = nombre;
            Apellido = apellido;
            MatriculaMedica = matriculaMedica;
            Especialidad = especialidad;
            Correo = correo;
            ClaveHash = BCrypt.Net.BCrypt.HashPassword(clave);
        }

        public bool Autenticar(string clave)
        {
            return BCrypt.Net.BCrypt.Verify(clave, ClaveHash);
        }
    }
}
