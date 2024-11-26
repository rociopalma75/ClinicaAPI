namespace Clinica_TFI.Models
{
    public class Paciente
    {
        public string Dni { get; set; }
        public string Cuil {  get; set; }
        public DateOnly FechaNacimiento { get; set; }
        public string Email { get; set; }
        public string Telefono {  get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Domicilio { get; set; }
        public HistoriaClinica HistoriaClinica { get; set; }

        public Paciente(string dni, string cuil, DateOnly fechaNacimiento, string email, string telefono, string nombre, string apellido, string domicilio)
        {
            Dni = dni;
            Cuil = cuil;
            FechaNacimiento = fechaNacimiento;
            Email = email;
            Telefono = telefono;
            Nombre = nombre;
            Apellido = apellido;
            Domicilio = domicilio;
            HistoriaClinica = new HistoriaClinica();
        }

        public Paciente(string dni, string cuil, DateOnly fechaNacimiento, string email, string telefono, string nombre, string apellido, string domicilio, List<Diagnostico> diagnosticosPrevios)
        {
            Dni = dni;
            Cuil = cuil;
            FechaNacimiento = fechaNacimiento;
            Email = email;
            Telefono = telefono;
            Nombre = nombre;
            Apellido = apellido;
            Domicilio = domicilio;
            HistoriaClinica = new HistoriaClinica(diagnosticosPrevios);
        }

        public void AddEvolucion(string diagnostico, Medico medico, string informe) => this.HistoriaClinica.AddEvolucion(diagnostico, medico, informe);
        public void AddDiagnostico(string nombreDiagnostico) => this.HistoriaClinica.AddDiagnostico(nombreDiagnostico);
    }
}
