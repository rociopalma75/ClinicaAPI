namespace Clinica_TFI.Domain
{
    public class PedidoLaboratorio
    {
        public Medico Medico { get; set; }
        public string Descripcion {  get; set; }
    
        public PedidoLaboratorio(Medico medico, string descripcion)
        {
            Medico = medico;
            Descripcion = descripcion;
        }
    }
}
