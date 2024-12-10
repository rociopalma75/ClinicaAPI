using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Moq;
using Xunit;
using Clinica_TFI.Application;
using Clinica_TFI.Domain;
using Clinica_TFI.Application.DTO;
using System;
using Clinica_TFI.Domain.Contracts;


namespace Test.Aplication
{
    public class UsuarioServiceTest
    {
        private readonly Mock<IClinicaRepository> _mockRepo;
        private readonly UsuarioService _usuarioService;

        public UsuarioServiceTest()
        {
            _mockRepo = new Mock<IClinicaRepository>();
            _usuarioService = new UsuarioService(_mockRepo.Object, null);
        }

        [Fact]
        public void AutenticarUsuario_ValidarCredenciales_ReturnToken()
        {

            // Arrange
            var medico = new Medico("John", "Doe", "12315", "Cardiologo", "johndoe@gmail.com", "pass1234");
            _mockRepo.Setup(repo => repo.GetMedicoByCorreo("johndoe@gmail.com")).Returns(medico);

            var credenciales = new MedicoLogInDTO
            {
                Correo = "johndoe@gmail.com",
                Clave = "pass1234"
            };

            // Act
            var token = _usuarioService.AutenticarMedico(credenciales);

            //Assert
            Assert.NotNull(token); //verifica que se haya generado el token
        }

        [Fact]

        public void AutenticarMedicoConContraseniaInvalida()
        {
            //Arrange
            var medico = new Medico("John", "Doe", "12315", "Cardiologo", "Johndoe@gmail.com", "pass1234");
            _mockRepo.Setup(repo => repo.GetMedicoByCorreo("johndoe@gmail.com")).Returns(medico);

            var credenciales = new MedicoLogInDTO
            {
                Correo = "johndoe@gmail.com",
                Clave = "malpass12345"
            };

            Assert.Throws<ArgumentException>(() => _usuarioService.AutenticarMedico(credenciales));

        }

        [Fact]

        public void AutenticarMedicoMedicoNoEncontrado()
        {
            _mockRepo.Setup(repo => repo.GetMedicoByCorreo("noexist@gmail.com")).Returns((Medico)null);

            var credenciales = new MedicoLogInDTO
            {
                Correo = "noexist@gmail.com",
                Clave = "pass1234"
            };

            //Act & Assert

            Assert.Throws<ArgumentException>(() => _usuarioService.AutenticarMedico(credenciales));
        }
    }
}
