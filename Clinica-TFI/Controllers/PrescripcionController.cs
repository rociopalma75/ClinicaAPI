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
    public class PrescripcionController : ControllerBase
    {
        private readonly PrescripcionService _prescripcionService;

        public PrescripcionController(PrescripcionService prescripcionService)
        {
            _prescripcionService = prescripcionService;
        }

        [HttpPost("Pacientes/{dniPaciente}/Diagnosticos/{diagnostico}/Evoluciones/{idEvolucion}/Receta")]
        public async Task<ActionResult<Paciente>> AddRecetaDigital(string dniPaciente, string diagnostico, int idEvolucion, [FromBody] RecetaDigitalRequestDTO requestReceta)
        {
            if (!dniPaciente.All(c => char.IsDigit(c))) throw new ArgumentException("Ingreso un dni con formato invalido");


            try
            {
                Paciente paciente = await _prescripcionService.AddRecetaDigital(dniPaciente,diagnostico, idEvolucion, requestReceta);
                return Ok(paciente);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPost("Pacientes/{dniPaciente}/Diagnosticos/{diagnostico}/Evoluciones/{idEvolucion}/Pedido")]
        public async Task<ActionResult<Paciente>> AddPedidoLaboratorio(string dniPaciente, string diagnostico, int idEvolucion, [FromBody] PedidoLaboratorioRequestDTO pedidoRequest)
        {
            if (!dniPaciente.All(c => char.IsDigit(c))) throw new ArgumentException("Ingreso un DNI con formato invalido");
            
            var medicoJson = User.FindFirst("Sesion").Value;
            if (string.IsNullOrEmpty(medicoJson)) return Unauthorized("No se pudo identificar el medico");
            Medico medico = JsonSerializer.Deserialize<Medico>(medicoJson);

            try
            {
                Paciente paciente = _prescripcionService.AddPedidoLaboratorio(dniPaciente, diagnostico, idEvolucion, medico, pedidoRequest);
                return Ok(paciente);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }

        }
    }
}
