namespace Clinica_TFI.Domain
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

        public int GenerarIdEvolucion() => this.Evoluciones.Count + 1;
        public Evolucion? GetEvolucionById(int id) => this.Evoluciones.Where(e => e.Id == id).FirstOrDefault(); 


        public void AddEvolucion(Medico medico, string informe)
        {
            int idEvolucion = GenerarIdEvolucion();
            Evolucion evolucion = new Evolucion(idEvolucion, informe, medico);
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

        public void AddEvolucionPlantilla(Medico medico, CatalogoPlantillas plantilla, dynamic request)
        {
            //Verificar las propiedades del request
            bool existsProperties = plantilla.VerificarPropiedades(request);

            if (!existsProperties) throw new ArgumentException("Error de formato de plantilla");

            int idEvolucion = GenerarIdEvolucion();

            Evolucion evolucion = new Evolucion(idEvolucion, request, medico);
            this.Evoluciones.Add(evolucion);
        }

        public void AddRecetaDigital(int idEvolucion, List<Medicamento> medicamentos, string observacionesMedicas)
        {
            Evolucion? evolucionEncontrada = GetEvolucionById(idEvolucion) ?? throw new Exception($"No se encuentra la evolucion con ID {idEvolucion}");

            if (evolucionEncontrada.ExistsRecetaDigital()) throw new Exception($"La evolución con ID {idEvolucion} ya tiene una receta digital registrada");

            evolucionEncontrada.AddRecetaDigital(medicamentos, observacionesMedicas);
        }

        public void AddPedidoLaboratorio(int idEvolucion, Medico medico, string pedidoRequest)
        {
            Evolucion? evolucionEncontrada = GetEvolucionById(idEvolucion) ?? throw new Exception($"No se encuentra la evolucion con ID {idEvolucion}");

            evolucionEncontrada.AddPedidoLaboratorio(medico, pedidoRequest);
        }
    }
}
