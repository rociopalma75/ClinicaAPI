namespace Clinica_TFI.Models
{
    public class Diagnostico
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public List<Evolucion> Evoluciones { get; set; }

        public Diagnostico(int id, string descripcion)
        {
            Id = id;
            Descripcion = descripcion;
            Evoluciones = new List<Evolucion>();
        }

        public void AddEvolucion(Medico medico, string informe)
        {
            Evolucion evolucion = new Evolucion(informe, medico);
            this.Evoluciones.Add(evolucion);
        }

        public bool ExistEvolucion (Medico medico, string informe)
        {
            return this.Evoluciones.Any(e => e.ExistEvolucion(medico, informe));
        }

        public bool ExistDiagnostico (string nombreDiagnostico)
        {
            return this.Descripcion == nombreDiagnostico;
        }

    }
}
