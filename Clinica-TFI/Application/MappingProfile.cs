using AutoMapper;
using Clinica_TFI.Application.DTO;
using Clinica_TFI.Models;

namespace Clinica_TFI.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PacienteRequestDTO, Paciente>();
            //CreateMap<DiagnosticoRequestDTO, Diagnostico>();
            //CreateMap<EvolucionRequestDTO, Evolucion>();
            //
            CreateMap<Medico, MedicoResponseDTO>();
            //CreateMap<MedicoRequestDTO, Medico>();
        }
    }
}
