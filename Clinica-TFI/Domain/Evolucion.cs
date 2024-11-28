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

        public bool ExistsRecetaDigital() => this.RecetaDigital != null;
        public bool ExistsPedidoLaboratorio() => this.PedidoLaboratorio != null;

        public bool MayorA48Horas()
        {
            TimeSpan difference = DateTime.Now.Subtract(this.FechaHora);

            if(difference.TotalHours > 48) return true;
            return false;
        }

        public void AddRecetaDigital(Medico medico, List<Medicamento> medicamentos, string observacionesMedicas)
        {
            if (MayorA48Horas()) throw new Exception("Transcurrieron más de 48 hs, no se puede editar la evolución");
           
            this.RecetaDigital = new RecetaDigital(medicamentos, observacionesMedicas, medico);
        }

        public void AddPedidoLaboratorio(Medico medico, string pedidoRequest)
        {
            //Validar lo de las 48hs
            if (MayorA48Horas()) throw new Exception("Transcurrieron más de 48 hs, no se puede editar la evolución");

            this.PedidoLaboratorio = new PedidoLaboratorio(medico, pedidoRequest);
        }

    }
}
