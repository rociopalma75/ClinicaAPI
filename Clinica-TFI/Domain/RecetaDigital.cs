using Clinica_TFI.Domain;

namespace Clinica_TFI.Domain
{
    public class RecetaDigital
    {
        public DateTime FechaHora { get; set; }
        public Medico Medico { get; set; }
        public string ObservacionesMedicas { get; set; }
        public List<Medicamento> Medicamentos { get; set; }

        //public string NroAfiliado {  get; set; }
        //public string Diagnostico {  get; set; }
        //public string NombreApellidoPaciente { get; set; }
        //public string ObraSocial { get; set; }

        public RecetaDigital(List<Medicamento> medicamentos, string observacionesMedicas, Medico medico)
        {
            FechaHora = DateTime.Now;
            Medico = medico;
            Medicamentos = medicamentos;
            ObservacionesMedicas = observacionesMedicas;
        }
    }
}
