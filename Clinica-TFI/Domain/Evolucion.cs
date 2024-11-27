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
        public List<PedidoLaboratorio>? PedidoLaboratorio { get; set; } = null;

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
        public int GenerarIdPedidoLaboratorio() => this.PedidoLaboratorio.Count + 1;
        public bool ExistEvolucion(Medico medico, string informe)
        {
            return this.Medico.Equals(medico) && this.Informe.Equals(informe);
        }

        public bool ExistsRecetaDigital() => this.RecetaDigital != null;
        public bool ExistsPedidoLaboratorio() => this.PedidoLaboratorio != null;

        public void AddRecetaDigital(List<Medicamento> medicamentos, string observacionesMedicas)
        {
            //Validar lo de las 48hs 
            this.RecetaDigital = new RecetaDigital(medicamentos, observacionesMedicas, this.Medico);
        }

        public void AddPedidoLaboratorio(Medico medico, string pedidoRequest)
        {
            //Validar lo de las 48hs
            if (!ExistsPedidoLaboratorio()) this.PedidoLaboratorio = new List<PedidoLaboratorio>();

            int idPedido = GenerarIdPedidoLaboratorio();

            PedidoLaboratorio pedidoNuevo = new PedidoLaboratorio(idPedido, medico, pedidoRequest);
            this.PedidoLaboratorio?.Add(pedidoNuevo);
        }

    }
}
