using Clinica_TFI.Application;
using Clinica_TFI.Application.DTO;
using Clinica_TFI.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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
        [SwaggerOperation(Summary = "Listar las historias clínicas de todos los pacientes")]
        [HttpGet("Pacientes")]
        public async Task<ActionResult<List<Paciente>>> GetPacientes()
        {
            List<Paciente> pacientes = _clinicaService.GetPacientes();
            return Ok(pacientes);
        }

        [SwaggerOperation(Summary = "Listar la historia clínica de un paciente")]
        [HttpGet("Pacientes/{dniPaciente}")] 
        public async Task<ActionResult<Paciente?>> GetPacienteByDni(string dniPaciente)
        {
            if (dniPaciente.Any(c => !char.IsDigit(c))) return BadRequest("Ingreso un dni con formato invalido");

            Paciente? paciente = _clinicaService.GetPacienteByDni(dniPaciente);

            if (paciente == null) return NotFound($"Paciente con {dniPaciente} no encontrado");

            return Ok(paciente);
        }

        [SwaggerOperation(Summary = "Registrar paciente")]
        [HttpPost("Pacientes")]
        public async Task<ActionResult<Paciente?>> PostPaciente(PacienteRequestDTO pacienteRequest)
        {
            Paciente pacienteCreado = _clinicaService.CreatePaciente(pacienteRequest);

            return Ok(pacienteCreado);
        }

        [SwaggerOperation(Summary = "Agregar una evolución de texto libre a un paciente")]
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
                Paciente paciente = _clinicaService.AddEvolucionTextoLibre(dniPaciente, diagnostico, medico, evolRequest);
                return Ok(paciente);
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
            catch (Exception ex){ return BadRequest(ex.ToString()); }              
        }

        [SwaggerOperation(Summary = "Agregar un nuevo diagnóstico a un paciente")]
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

        [SwaggerOperation(Summary = "Listar las plantillas de evolución")]
        [HttpGet("CatalogoPlantillas")]
        public async Task<ActionResult<List<CatalogoPlantillas>>> GetPlantillas()
        {
            List<CatalogoPlantillas> catalogoPlantillas = _clinicaService.GetCatalogoPlantillas();
            return Ok(catalogoPlantillas);
        }


        [SwaggerOperation(Summary = "Agregar una evolución con una plantilla en lugar de texto libre")]
        [HttpPost("Pacientes/{dniPaciente}/Diagnosticos/{diagnostico}/Plantilla/{idPlantilla}")]
        public async Task<ActionResult<Paciente>> AddEvolucionPlantilla(string dniPaciente, string diagnostico, int idPlantilla, [FromBody] dynamic request)
        {
            if (!dniPaciente.All(c => char.IsDigit(c))) throw new ArgumentException("Ingreso un DNI con formato invalido");

            var medicoJson = User.FindFirst("Sesion").Value;
            if (string.IsNullOrEmpty(medicoJson)) return Unauthorized("No se pudo identificar el medico");
            Medico medico = JsonSerializer.Deserialize<Medico>(medicoJson);

            try
            {
                Paciente paciente = _clinicaService.AddEvolucionPlantilla(dniPaciente, diagnostico, medico, idPlantilla, request);

                return Ok(paciente);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
