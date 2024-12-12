using System;

namespace Clinica_TFI.Domain
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
        public ObraSocial? ObraSocialPaciente {  get; set; }
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
            this.ObraSocialPaciente = new ObraSocial()
            {
                Codigo = "106005",
                Denominacion = "OBRA SOCIAL DEL PERSONAL DE ENTIDADES DEPORTIVAS Y CIVILES",
                Sigla = "OSPEDYC"
            };
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

        public void AddEvolucionPlantilla(string diagnostico, Medico medico, CatalogoPlantillas plantilla,dynamic request)
        {
            this.HistoriaClinica.AddEvolucionPlantilla(diagnostico, medico,plantilla,request);  
        }

        public void AddRecetaDigital(string diagnostico, int idEvolucion, Medico medico, List<Medicamento> medicamentos, string observacionesMedicas) 
        {
            this.HistoriaClinica.AddRecetaDigital(diagnostico, idEvolucion, medico, medicamentos, observacionesMedicas);
        }

        public void AddPedidoLaboratorio(string diagnostico, int idEvolucion, Medico medico, string pedidoRequest)
        {
            this.HistoriaClinica.AddPedidoLaboratorio(diagnostico, idEvolucion, medico, pedidoRequest);
        }

    }
}
