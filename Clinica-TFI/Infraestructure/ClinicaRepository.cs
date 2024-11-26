using Clinica_TFI.Domain.Contracts;
using Clinica_TFI.Models;

namespace Clinica_TFI.Infraestructure
{
    public class ClinicaRepository : IClinicaRepository
    {
        private readonly ClinicaContext _contextoClinica;

        public ClinicaRepository(ClinicaContext contextoClinica)
        {
            _contextoClinica = contextoClinica;
        }

        public bool ExistsMedico(string correo) => _contextoClinica.Medicos.Exists(m => m.Correo == correo);

        public bool ExistsPaciente(string dni) => _contextoClinica.Pacientes.Exists(p => p.Dni == dni);

        public List<Medico> GetMedicos() => _contextoClinica.Medicos;
        public Paciente? GetPacienteByDni(string dniPaciente) => _contextoClinica.Pacientes.Where(p => p.Dni.Equals(dniPaciente)).FirstOrDefault();
        public List<Paciente> GetPacientes() => _contextoClinica.Pacientes;

        public void CreatePaciente(Paciente paciente)
        {
            _contextoClinica.Pacientes.Add(paciente);
        }

        public void UpdatePaciente(Paciente paciente)
        {
            //"Se actualiza en db"
        }

        public void RegisterMedico(Medico medico)
        {
            int idMedico = _contextoClinica.Medicos.Count + 1;
            medico.Id = idMedico;
            _contextoClinica.Medicos.Add(medico);
        }

        public Medico? GetMedicoByCorreo(string correo) => _contextoClinica.Medicos.Where(m => m.Correo == correo).FirstOrDefault();


    }
}
