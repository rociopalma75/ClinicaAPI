namespace Clinica_TFI.Models
{
    public class Medico
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Especialidad { get; set; }
        public string Correo {  get; set; }
        public string ClaveHash { get; set; }

        public Medico()
        {
            
        }

        public Medico(string nombre, string apellido, string especialidad, string correo, string clave)
        {
            Nombre = nombre;
            Apellido = apellido;
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
