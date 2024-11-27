using Clinica_TFI.Domain;

namespace Clinica_TFI.Domain
{
    public class Evolucion
    {
        public int Id { get; set; }
        public Medico Medico { get; set; }
        public DateTime FechaHora { get; set; }
        public string? Informe { get; set; }
        public dynamic? Plantilla { get; set; }
        public RecetaDigital? RecetaDigital { get; set; } = null;
        public PedidoLaboratorio? PedidoLaboratorio { get; set; } = null;

        public Evolucion(int id, string informe, Medico medico)
        {
            Id = id;
            Informe = informe;
            Medico = medico;
            FechaHora = DateTime.Now;
        }

        public Evolucion(int id, dynamic plantilla, Medico medico)
        {
            Id = id;
            Plantilla = plantilla;
            Medico = medico;
            FechaHora = DateTime.Now;
        }

        public bool ExistEvolucion(Medico medico, string informe)
        {
            return this.Medico.Equals(medico) && this.Informe.Equals(informe);
        }

        public bool ExitsRecetaDigital() => this.RecetaDigital != null;

        public void AddRecetaDigital(List<Medicamento> medicamentos, string observacionesMedicas)
        {
            //Validar lo de las 48hs 
            this.RecetaDigital = new RecetaDigital(medicamentos, observacionesMedicas, this.Medico);
        }

    }
}
