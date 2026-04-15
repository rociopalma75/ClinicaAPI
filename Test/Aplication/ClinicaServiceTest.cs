using Clinica_TFI.Application;
using Clinica_TFI.Application.DTO;
using Clinica_TFI.Domain;
using Clinica_TFI.Domain.Contracts;
using Moq;
using System.Net;

namespace Test.Aplication
{
    public class ClinicaServiceTest
    {
        private readonly Mock<IClinicaRepository> _mockRepo;
        private readonly ClinicaService _clinicaService;
        
        public ClinicaServiceTest()
        {
            _mockRepo = new Mock<IClinicaRepository>();
            _clinicaService = new ClinicaService(_mockRepo.Object, null, null);
        }

        [Fact]
        public void ObtenerPacientes_RetornaListaDePacientes()
        {
            // Arrange
            var pacientes = new List<Paciente>
            {
                new Paciente { Dni = "12345", Nombre = "John", Apellido = "Doe" },
                new Paciente { Dni = "67890", Nombre = "Jane", Apellido = "Doe" }
            };
            _mockRepo.Setup(repo => repo.GetPacientes()).Returns(pacientes);

            // Act
            var result = _clinicaService.GetPacientes();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("12345", result[0].Dni);
            Assert.Equal("67890", result[1].Dni);
        }

        [Fact]
        public void AgregarPaciente_DNIYaExistente_RetornaExcepcion()
        {
            // Arrange
            string dni = "12345";
            _mockRepo.Setup(repo => repo.ExistsPaciente(dni)).Returns(true);

            // Act & Assert
            PacienteRequestDTO nuevoPaciente = CrearPaciente(dni, "20-12345678-9", new DateOnly(1990, 1, 1), "1234567890", "John", "Doe", "Salta 123");

            var exception = Assert.ThrowsAsync<ArgumentException>(() => _clinicaService.CreatePaciente(nuevoPaciente));
            Assert.Equal($"El paciente con DNI {nuevoPaciente.Dni} ya está registrado", exception.Result.Message);

        }

        #region Metodos Privados
        public PacienteRequestDTO CrearPaciente(string dni, string cuil, DateOnly fechaNacimiento,string telefono, string nombre, string apellido, string domicilio)
        {
            return new PacienteRequestDTO
            {
                Dni = dni,
                Cuil = cuil,
                FechaNacimiento = fechaNacimiento,
                Email = $"test{dni}@example.com",
                Telefono = telefono,
                Nombre = nombre,
                Apellido = apellido,
                Domicilio = domicilio,
            };
        }
        #endregion
    }
}