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

        public bool ExistEvolucion (Medico medico, string informe)
        {
            return this.Evoluciones.Any(e => e.ExistEvolucion(medico, informe));
        }

        public bool ExistDiagnostico (string nombreDiagnostico)
        {
            return this.Descripcion == nombreDiagnostico;
        }

        public void AddEvolucion(Medico medico, string informe)
        {
            int idEvolucion = GenerarIdEvolucion();
            Evolucion evolucion = new Evolucion(idEvolucion, informe, medico);
            this.Evoluciones.Add(evolucion);
        }

        public void AddEvolucionPlantilla(Medico medico, CatalogoPlantillas plantilla, dynamic request)
        {
            //Verifica las propiedades del request
            bool existsProperties = plantilla.VerificarPropiedades(request);

            if (!existsProperties) throw new ArgumentException("Error de formato de plantilla");

            int idEvolucion = GenerarIdEvolucion();

            Evolucion evolucion = new Evolucion(idEvolucion, request, medico);
            this.Evoluciones.Add(evolucion);
        }

        public void AddRecetaDigital(int idEvolucion, Medico medico, List<Medicamento> medicamentos, string observacionesMedicas)
        {
            Evolucion? evolucionEncontrada = GetEvolucionById(idEvolucion) ?? throw new Exception($"No se encuentra la evolucion con ID {idEvolucion}");

            if (evolucionEncontrada.ExistsRecetaDigital()) throw new Exception($"La evolución con ID {idEvolucion} ya tiene una receta digital registrada");

            if (evolucionEncontrada.ExistsPedidoLaboratorio()) throw new Exception("Operación no permitida ya que el paciente cuenta con un pedido de laboratorio registrado.");

            evolucionEncontrada.AddRecetaDigital(medico, medicamentos, observacionesMedicas);
        }

        public void AddPedidoLaboratorio(int idEvolucion, Medico medico, string pedidoRequest)
        {
            Evolucion? evolucionEncontrada = GetEvolucionById(idEvolucion) ?? throw new Exception($"No se encuentra la evolucion con ID {idEvolucion}");

            if (evolucionEncontrada.ExistsPedidoLaboratorio()) throw new Exception($"La evolución con ID {idEvolucion} ya tiene un pedido de laboratorio registrado.");

            if (evolucionEncontrada.ExistsRecetaDigital()) throw new Exception("Operación no permitida, ya que el paciente cuenta con una receta dígital registrada.");
            
            evolucionEncontrada.AddPedidoLaboratorio(medico, pedidoRequest);
        }
    }
}
