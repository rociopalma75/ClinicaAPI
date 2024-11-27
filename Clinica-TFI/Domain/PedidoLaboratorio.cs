namespace Clinica_TFI.Domain
{
    public class PedidoLaboratorio
    {
        public int Id { get; set; }
        public Medico Medico { get; set; }
        public string Descripcion {  get; set; }
    
        public PedidoLaboratorio(int id, Medico medico, string descripcion)
        {
            Id = id;
            Medico = medico;
            Descripcion = descripcion;
        }
    }
}
