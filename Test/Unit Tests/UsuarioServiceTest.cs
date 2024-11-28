using AutoMapper;
using Clinica_TFI.Application;
using Clinica_TFI.Application.DTO;
using Clinica_TFI.Domain;
using Clinica_TFI.Domain.Contracts;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Unit_Tests
{
    public class UsuarioServiceTest
    {
        private UsuarioService _usuarioService;
        private IClinicaRepository _clinicaRepository = Substitute.For<IClinicaRepository>();
        private IMapper _mapper = Substitute.For<IMapper>();
        private MedicoRequestDTO _medicoRequest = null!;
        private MedicoLogInDTO _medicoLogin = null!;
        public UsuarioServiceTest()
        {
            _medicoRequest = new MedicoRequestDTO()
            {
                Nombre = "Sabrina",
                Apellido = "Cedron",
                MatriculaMedica = "12123/cl",
                Especialidad = "Clinico",
                Correo = "sabrina@gmail.com",
                Clave = "123434"
            };
            _medicoLogin = new MedicoLogInDTO()
            {
                Correo = "sabrina@gmail.com",
                Clave = "123434"
            };
            _usuarioService = new UsuarioService(_clinicaRepository, _mapper);
        }

        [Fact]
        public void RegistrarMedicoConCorreoNoExistente()
        {
            Medico medicoSimulado = new Medico(
                nombre: _medicoRequest.Nombre,
                apellido: _medicoRequest.Apellido,
                matriculaMedica: _medicoRequest.MatriculaMedica,
                especialidad: _medicoRequest.Especialidad,
                correo: _medicoRequest.Correo,
                clave: _medicoRequest.Clave
                );
            _clinicaRepository.RegisterMedico(medicoSimulado);
            MedicoResponseDTO medicoEsperado = _mapper.Map<MedicoResponseDTO>( medicoSimulado );

            MedicoResponseDTO medicoResult = _usuarioService.RegisterMedico(_medicoRequest);

            Assert.Equal(medicoEsperado, medicoResult);
        }

        [Fact]
        public void RegistrarMedicoConCorreoExistente()
        {
            _clinicaRepository.ExistsMedico(_medicoRequest.Correo).Returns(true);

            var exception = Assert.Throws<Exception>(() => _usuarioService.RegisterMedico(_medicoRequest));

            Assert.Equal($"El correo {_medicoRequest.Correo} ya fue registrado", exception.Message);
        }

        [Fact]
        public void AutenticarMedicoExistente()
        {
            Medico medicoSimulado = new Medico(
                nombre: _medicoRequest.Nombre,
                apellido: _medicoRequest.Apellido,
                matriculaMedica: _medicoRequest.MatriculaMedica,
                especialidad: _medicoRequest.Especialidad,
                correo: _medicoRequest.Correo,
                clave: _medicoRequest.Clave
                );

            _clinicaRepository.GetMedicoByCorreo(_medicoLogin.Correo).Returns( medicoSimulado );

            string tokenResultado = _usuarioService.AutenticarMedico(_medicoLogin);

            Assert.NotNull(tokenResultado);
            Assert.IsType<string>(tokenResultado);
        }


        [Fact]
        public void AutenticarMedicoNoExistente()
        {
            _clinicaRepository.GetMedicoByCorreo(_medicoLogin.Correo).ReturnsNull();

            var exception = Assert.Throws<ArgumentException>(() => _usuarioService.AutenticarMedico(_medicoLogin));

            Assert.Equal($"El {_medicoLogin.Correo} no se encuentra registrado", exception.Message);
        }

        [Fact]
        public void AutenticarMedicoConClaveIncorrecta()
        {
            Medico medicoSimulado = new Medico(
                nombre: _medicoRequest.Nombre,
                apellido: _medicoRequest.Apellido,
                matriculaMedica: _medicoRequest.MatriculaMedica,
                especialidad: _medicoRequest.Especialidad,
                correo: _medicoRequest.Correo,
                clave: _medicoRequest.Clave
            );

            _clinicaRepository.GetMedicoByCorreo(_medicoLogin.Correo).Returns(medicoSimulado);

            _medicoLogin.Clave = "admin";

            var exception = Assert.Throws<ArgumentException>(() => _usuarioService.AutenticarMedico(_medicoLogin));

            Assert.Equal("Clave incorrecta.", exception.Message);
        }
    }
}
