using Clinica_TFI.Application;
using Clinica_TFI.Application.DTO;
using Clinica_TFI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using System.Text.Json;

namespace Clinica_TFI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClinicaController : ControllerBase
    {
        private readonly ClinicaService _clinicaService;
        public ClinicaController(ClinicaService clinicaService)
        {
            _clinicaService = clinicaService;
        }

        [HttpGet("Pacientes")]
        public async Task<ActionResult<List<Paciente>>> GetPacientes()
        {
            List<Paciente?> pacientes = _clinicaService.GetPacientes();
            return Ok(pacientes);
        }

        [HttpGet("Pacientes/{dniPaciente}")] 
        public async Task<ActionResult<Paciente?>> GetPacienteByDni(string dniPaciente)
        {
            if (dniPaciente.Any(c => !char.IsDigit(c))) return BadRequest("Ingreso un dni con formato invalido");

            Paciente? paciente = _clinicaService.GetPacienteByDni(dniPaciente);

            if (paciente == null) return NotFound($"Paciente con {dniPaciente} no encontrado");

            return Ok(paciente);
        }

        [HttpPost("Pacientes")]
        public async Task<ActionResult<Paciente?>> PostPaciente(PacienteRequestDTO pacienteRequest)
        {
            Paciente pacienteCreado = _clinicaService.CreatePaciente(pacienteRequest);

            return Ok(pacienteCreado);
        }

        [HttpPost("Pacientes/{dniPaciente}/Diagnosticos/{diagnostico}")]
        public async Task<ActionResult<Paciente?>> AddEvolucion(string dniPaciente, string diagnostico, [FromBody] EvolucionRequestDTO evolRequest)
        {
            if (dniPaciente.Any(c => !char.IsDigit(c))) return BadRequest("Ingreso un dni con formato invalido");

            //Validar si se ingresa un diagnostico que contengan digitos. (Para mi todo tiene que ser con caracteres alfabeticos)

            var medicoJson = User.FindFirst("Sesion").Value;
            if (string.IsNullOrEmpty(medicoJson)) return Unauthorized("No se pudo identificar el medico");
            Medico medico = JsonSerializer.Deserialize<Medico>(medicoJson);

            try
            {
                Paciente paciente = _clinicaService.AddEvolucion(dniPaciente, diagnostico, medico, evolRequest);
                return Ok(paciente);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            } 
        }

        [HttpPost("Pacientes/{dniPaciente}/Diagnosticos")]
        public async Task<ActionResult<Paciente>> AddDiagnostico(string dniPaciente, [FromBody] DiagnosticoRequestDTO diagnosticoRequest)
        {
            if (!dniPaciente.All(c => char.IsDigit(c))) return BadRequest("Ingreso un DNI con formato invalido");

            try
            {
                Paciente? paciente = _clinicaService.AddDiagnostico(diagnosticoRequest, dniPaciente);
                return Ok(paciente);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
