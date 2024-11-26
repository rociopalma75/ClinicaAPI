namespace Clinica_TFI.Models
{
    public class Evolucion
    {
        public string Informe {  get; set; }
        public Medico Medico { get; set; } 


        public Evolucion(string informe, Medico medico)
        {
            Informe = informe;
            Medico = medico;
        }

        public bool ExistEvolucion(Medico medico, string informe)
        {
            return this.Medico.Equals(medico) && this.Informe.Equals(informe);
        }
    }
}
