using Clinica_TFI.Models;

namespace Clinica_TFI.Domain.Contracts
{
    public interface IClinicaRepository
    {
        List<Paciente> GetPacientes();
        List<Medico> GetMedicos();
        bool ExistsMedico(string correo);
        bool ExistsPaciente(string dni);
        void CreatePaciente(Paciente paciente);
        Paciente? GetPacienteByDni(string dniPaciente);
        Medico? GetMedicoByCorreo(string correo);
        void UpdatePaciente(Paciente paciente);
        void RegisterMedico(Medico medico);
    }
}
