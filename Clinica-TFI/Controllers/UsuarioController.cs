using Clinica_TFI.Application;
using Clinica_TFI.Application.DTO;
using Clinica_TFI.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Clinica_TFI.Controllers
{
    [Route("api/Clinica")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("Medico/Register")]
        public async Task<ActionResult<MedicoResponseDTO>> RegisterMedico(MedicoRequestDTO medicoRequest)
        {
            try
            {
                MedicoResponseDTO medico = _usuarioService.RegisterMedico(medicoRequest);
                return Ok(medico);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Medico/Login")]
        public async Task<ActionResult> LoginMedico(MedicoLogInDTO credenciales)
        {
            try
            {
                string tokenGenerado = _usuarioService.AutenticarMedico(credenciales);


                return Ok(new { message = "Usuario autorizado", token = tokenGenerado });
            }
            catch (ArgumentException ex)
            {
                return Unauthorized(ex.Message);
            } 

        }
    }
}
