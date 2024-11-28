using Clinica_TFI.Domain;

namespace Clinica_TFI.Domain.Contracts
{
    public interface IClinicaRepository
    {
        List<Paciente> GetPacientes();
        List<Medico> GetMedicos();
        List<CatalogoPlantillas> GetCatalogoPlantillas();
        bool ExistsMedico(string correo);
        bool ExistsPaciente(string dni);
        Paciente? GetPacienteByDni(string dniPaciente);
        Medico? GetMedicoByCorreo(string correo);
        CatalogoPlantillas? GetCatalogoPlantillaById(int id);
        void CreatePaciente(Paciente paciente);
        void UpdatePaciente(Paciente paciente);
        void RegisterMedico(Medico medico);
    }
}
