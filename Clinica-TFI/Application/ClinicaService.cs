using AutoMapper;
using Clinica_TFI.Application.DTO;
using Clinica_TFI.Domain.Contracts;
using Clinica_TFI.Models;

namespace Clinica_TFI.Application
{
    public class ClinicaService
    {
        private readonly IClinicaRepository _clinicaRepository;
        private readonly IMapper _mapper;

        public ClinicaService(IClinicaRepository clinicaRepository, IMapper mapper)
        {
            _clinicaRepository = clinicaRepository;
            _mapper = mapper;
        }

        public List<Paciente> GetPacientes() => _clinicaRepository.GetPacientes();
        public Paciente? GetPacienteByDni(string dniPaciente) => _clinicaRepository.GetPacienteByDni(dniPaciente);


        public Paciente CreatePaciente(PacienteRequestDTO pacienteRequestDTO)
        {
            bool pacienteExists = _clinicaRepository.ExistsPaciente(pacienteRequestDTO.Dni);

            if (pacienteExists) throw new ArgumentException($"El paciente con DNI {pacienteRequestDTO.Dni} ya está registrado");

            Paciente pacienteCreado = _mapper.Map<Paciente>(pacienteRequestDTO);
            _clinicaRepository.CreatePaciente(pacienteCreado);
            return pacienteCreado;
        }
        public Paciente AddEvolucion(string dniPaciente, string diagnostico, Medico medico, EvolucionRequestDTO evolRequest)
        {
            Paciente? paciente = _clinicaRepository.GetPacienteByDni(dniPaciente) ?? throw new ArgumentException($"El paciente con DNI {dniPaciente} no se encuentra");

            paciente.AddEvolucion(diagnostico, medico, evolRequest.Informe);
            _clinicaRepository.UpdatePaciente(paciente);

            return paciente;
        }

        public Paciente AddDiagnostico(DiagnosticoRequestDTO diagRequest, string dniPaciente)
        {
            Paciente? paciente = _clinicaRepository.GetPacienteByDni(dniPaciente) ?? throw new ArgumentException($"El paciente con DNI {dniPaciente} no se encuentra");

            //Verificar si ya existe un diagnostico con ese nombre
            bool diagnosticoExist = paciente.HistoriaClinica.ExistDiagnostico(diagRequest.Descripcion);

            if (diagnosticoExist) throw new ArgumentException($"El diagnostico {diagRequest.Descripcion} ya se encuentra en la historia clinica del paciente {dniPaciente} ");

            paciente.AddDiagnostico(diagRequest.Descripcion);
            _clinicaRepository.UpdatePaciente(paciente);
            return paciente;
        }

        public Paciente AddRecetaDigital(string dniPaciente, string diagnostico, string )
        {

        }
    }
}
