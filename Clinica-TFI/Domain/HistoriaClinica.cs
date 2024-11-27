namespace Clinica_TFI.Domain
{
    public class HistoriaClinica
    {
        public List<Diagnostico> Diagnosticos { get; set; }

        public HistoriaClinica()
        {
            Diagnosticos = new List<Diagnostico>();
        }
        public HistoriaClinica(List<Diagnostico> diagnosticos)
        {
            this.Diagnosticos = diagnosticos;
        }

        public Diagnostico? GetDiagnostico(string nombreDiagnostico) => this.Diagnosticos.Where(d => d.Descripcion.Equals(nombreDiagnostico)).FirstOrDefault();

        public void AddEvolucion(string diagnostico, Medico medico, string informe)
        {
            Diagnostico? diagnosticoEncontrado = GetDiagnostico(diagnostico);
            
            if (diagnosticoEncontrado == null) throw new ArgumentException($"El diagnostico {diagnostico} no se encuentra");

            diagnosticoEncontrado.AddEvolucion(medico, informe);
        }

        public void AddDiagnostico(string nombreDiagnostico)
        {
            int idDiagnostico = GenerarIdDiagnostico();
            Diagnostico diagnostico = new Diagnostico(idDiagnostico, nombreDiagnostico);
            this.Diagnosticos.Add(diagnostico);
        }
        public int GenerarIdDiagnostico() => this.Diagnosticos.Count + 1;

        public bool ExistDiagnostico(string nombreDiagnostico)
        {
            return this.Diagnosticos.Any(d => d.ExistDiagnostico(nombreDiagnostico));
        }

        public void AddEvolucionPlantilla(string diagnostico, Medico medico, CatalogoPlantillas plantilla, dynamic request)
        {
            Diagnostico? diagnosticoEncontrado = GetDiagnostico(diagnostico);

            if (diagnosticoEncontrado == null) throw new ArgumentException($"El diagnóstico {diagnostico} no se encuentra");

            diagnosticoEncontrado.AddEvolucionPlantilla(medico, plantilla, request);
        }

        public void AddRecetaDigital(string diagnostico, int idEvolucion, List<Medicamento> medicamentos, string observacionesMedicas)
        {
            Diagnostico? diagnosticoEncontrado = GetDiagnostico(diagnostico);

            if (diagnosticoEncontrado == null) throw new ArgumentException($"El diagnóstico {diagnostico} no se encuentra");

            diagnosticoEncontrado.AddRecetaDigital(idEvolucion,  medicamentos,observacionesMedicas);

        }


    }
}
