using Clinica_TFI.Application;
using Clinica_TFI.Application.DTO;
using Clinica_TFI.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Clinica_TFI.Controllers
{
    [Route("api/Clinica")]
    [ApiController]
    [Authorize]
    public class RecetaController : ControllerBase
    {
        private readonly RecetaService _recetaService;

        public RecetaController(RecetaService recetaService)
        {
            _recetaService = recetaService;
        }

        [HttpPost("Pacientes/{dniPaciente}/Diagnostico/{diagnostico}/Evoluciones/{idEvolucion}/Receta")]
        public async Task<ActionResult<Paciente>> AddRecetaDigital(string dniPaciente, string diagnostico, int idEvolucion, [FromBody] RecetaDigitalRequestDTO requestReceta)
        {
            if (!dniPaciente.All(c => char.IsDigit(c))) throw new ArgumentException("Ingreso un dni con formato invalido");

            try
            {
                Paciente paciente = await _recetaService.AddRecetaDigital(dniPaciente,diagnostico, idEvolucion, requestReceta);
                return Ok(paciente);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

    }
}
