using AutoMapper;
using Clinica_TFI.Application.DTO;
using Clinica_TFI.Domain.Contracts;
using Clinica_TFI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using System.Text;

namespace Clinica_TFI.Application
{
    public class UsuarioService
    {
        private readonly IClinicaRepository _clinicaRepository;
        private readonly IMapper _mapper;

        public UsuarioService(IClinicaRepository clinicaRepository, IMapper mapper)
        {
            _clinicaRepository = clinicaRepository;
            _mapper = mapper;
        }


        public MedicoResponseDTO RegisterMedico(MedicoRequestDTO medicoRequest)
        {
            //Validar si ya existe el medico
            Medico medicoInserted = new Medico(medicoRequest.Nombre, medicoRequest.Apellido, medicoRequest.Especialidad, medicoRequest.Correo, medicoRequest.Clave);
            _clinicaRepository.RegisterMedico(medicoInserted);
            MedicoResponseDTO medicoResponse = _mapper.Map<MedicoResponseDTO>(medicoInserted);
            return medicoResponse;
        }

        public string AutenticarMedico(MedicoLogInDTO credenciales)
        {
            Medico medico = _clinicaRepository.GetMedicoByCorreo(credenciales.Correo) ?? throw new ArgumentException($"El {credenciales.Correo} no se encuentra registrado");
            bool verify = medico.Autenticar(credenciales.Clave);

            if (!verify) throw new ArgumentException("Clave incorrecta.");

            string token = GenerarTokenJWT(medico);

            return token;
        }

        public string GenerarTokenJWT(Medico medico)
        {
            var claims1 = new[]
{
                new Claim(ClaimTypes.Name, medico.Nombre),
                new Claim(ClaimTypes.Role, medico.Especialidad ?? "Clinico"),
                new Claim("Sesion", JsonSerializer.Serialize(medico)),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("IngenieraSoftware-TFI-Clinica2024."));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims1,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
             );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
