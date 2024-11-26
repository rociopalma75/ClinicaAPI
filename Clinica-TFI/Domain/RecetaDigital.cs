using Clinica_TFI.Models;

namespace Clinica_TFI.Domain
{
    public class RecetaDigital
    {
        public DateTime FechaHora { get; set; }
        public string NombreApellidoPaciente { get; set; }
        public string ObservacionesMedicas { get; set; }
        public string ObraSocial {  get; set; }
        public string NroAfiliado {  get; set; }
        public string Diagnostico {  get; set; }
        public Medico Medico { get; set; }
        public List<Medicamento> Medicamentos { get; set; }
    }
}
