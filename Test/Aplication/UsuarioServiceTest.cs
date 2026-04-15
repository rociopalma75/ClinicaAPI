using Moq;
using Clinica_TFI.Application;
using Clinica_TFI.Domain;
using Clinica_TFI.Application.DTO;
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
        public void AutenticarMedico_CredencialesValidas_RetornaToken()
        {

            // Arrange
            string correo = "usuario@gmail.com";
            var medico = new Medico("John", "Doe", "12315", "Cardiologo", correo, "pass1234");
            _mockRepo.Setup(repo => repo.GetMedicoByCorreo(correo)).Returns(medico);

            var credenciales = new MedicoLogInDTO
            {
                Correo = correo,
                Clave = "pass1234"
            };

            // Act
            var token = _usuarioService.AutenticarMedico(credenciales);

            //Assert
            Assert.NotNull(token); //verifica que se haya generado el token
        }

        [Fact]

        public void AutenticarMedico_CredencialesInvalidas_RetornaExcepcion()
        {
            //Arrange
            string correo = "johndoe@gmail.com";
            var medico = new Medico("John", "Doe", "12315", "Cardiologo", correo, "pass1234");
            _mockRepo.Setup(repo => repo.GetMedicoByCorreo(correo)).Returns(medico);

            var credenciales = new MedicoLogInDTO
            {
                Correo = correo,
                Clave = "malpass12345"
            };

            Assert.Throws<ArgumentException>(() => _usuarioService.AutenticarMedico(credenciales));

        }

        [Fact]

        public void AutenticarMedico_MedicoNoEncontrado_RetornaExcepcion()
        {
            string correoInexistente = "usuarioInexistente@gmail.com";
            _mockRepo.Setup(repo => repo.GetMedicoByCorreo(correoInexistente)).Returns((Medico)null);

            var credenciales = new MedicoLogInDTO
            {
                Correo = correoInexistente,
                Clave = "pass1234"
            };

            //Act & Assert

            Assert.Throws<ArgumentException>(() => _usuarioService.AutenticarMedico(credenciales));
        }
    }
}
