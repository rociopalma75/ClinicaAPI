using AutoMapper;
using Clinica_TFI.Application.Connected_Services;
using Clinica_TFI.Application.DTO;
using Clinica_TFI.Domain;
using Clinica_TFI.Domain.Contracts;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace Clinica_TFI.Application
{
    public class PrescripcionService
    {
        private readonly IClinicaRepository _clinicalRepository;
        private readonly ExternalAPIService _externalAPIService;
        private readonly IMapper _mapper;

        public PrescripcionService(IClinicaRepository clinicalRepository, ExternalAPIService externalAPIService, IMapper mapper)
        {
            _clinicalRepository = clinicalRepository;
            _externalAPIService = externalAPIService;
            _mapper = mapper;
        }

        public async Task<Paciente> AddRecetaDigital(string dniPaciente, string diagnostico, int idEvolucion, RecetaDigitalRequestDTO recetaRequest)
        {
            Paciente? paciente = _clinicalRepository.GetPacienteByDni(dniPaciente) ?? throw new Exception($"El paciente con DNI {dniPaciente} no se encuentra");

            if (!paciente.TieneObraSocial()) throw new Exception($"El paciente con DNI {dniPaciente} no tiene obra social");

            List<string> medicamentos = recetaRequest.Medicamentos.Split(',').ToList();

            if (medicamentos.Count > 2) throw new Exception("Se ingresaron más de 2 medicamentos");

            List<Medicamento> listaMedicamentos = await ObtenerMedicamentos(medicamentos);

            paciente.AddRecetaDigital(diagnostico, idEvolucion, listaMedicamentos, recetaRequest.Observaciones);

            _clinicalRepository.UpdatePaciente(paciente);
            return paciente;
        }

        public async Task<List<Medicamento>> ObtenerMedicamentos(List<string> medicamentos)
        {
            List<Medicamento> resultsMedicamentos = new List<Medicamento>();
            try
            {
                List<int> codigos = medicamentos.Select(int.Parse).ToList();
                resultsMedicamentos = await _externalAPIService.GetMedicamentosByCodigos(codigos);

            }
            catch (Exception e) { throw new Exception(e.Message); }   
            
            return resultsMedicamentos;
        }

        public Paciente AddPedidoLaboratorio(string dniPaciente, string diagnostico, int idEvolucion, Medico medico, PedidoLaboratorioRequestDTO pedidoRequest)
        {
            Paciente? paciente = _clinicalRepository.GetPacienteByDni(dniPaciente) ?? throw new Exception($"El paciente con DNI {dniPaciente} no se encuentra");

            //Para registrar pedido tiene que tener obra social?
            paciente.AddPedidoLaboratorio(diagnostico, idEvolucion, medico, pedidoRequest.Descripcion);

            _clinicalRepository.UpdatePaciente(paciente);
            return paciente;
        }



    }
}
