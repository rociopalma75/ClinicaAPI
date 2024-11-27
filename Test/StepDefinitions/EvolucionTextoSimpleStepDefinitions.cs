using AutoMapper;
using Clinica_TFI.Application;
using Clinica_TFI.Application.DTO;
using Clinica_TFI.Domain.Contracts;
using Clinica_TFI.Domain;
using NSubstitute;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Test.StepDefinitions
{
    [Binding]
    public class EvolucionTextoSimpleStepDefinitions
    {
        private Medico _medico = null!;
        private string _dniPaciente = null!;
        private EvolucionRequestDTO _evolucionIngresada = null!;
        private string _diagnosticoElegido = null!;
        private Paciente _pacienteResultado = null!;
        private ClinicaService _clinicaService = null!;
        private readonly IClinicaRepository _clinicalRepository = Substitute.For<IClinicaRepository>();
        private readonly IMapper _mapper = Substitute.For<IMapper>();

        [Before]
        public void Setup()
        {
            this._medico = null;
            this._dniPaciente = null;
            this._evolucionIngresada = null;
            this._diagnosticoElegido = null;
            this._pacienteResultado = null;
            this._clinicaService = new ClinicaService(_clinicalRepository, _mapper);
        }

        [Given("el medico {string} ha iniciado sesion.")]
        public void GivenElMedicoHaIniciadoSesion_(string nombreMedico)
        {
            _medico = new Medico(nombreMedico, "Torres", "Clinico", "rocio@gmail.com", "1234");
        }

        [Given("ha buscado la historia clinica del paciente {string} que posee los siguientes diagnosticos")]
        public void GivenHaBuscadoLaHistoriaClinicaDelPacienteQuePoseeLosSiguientesDiagnosticos(string dniPaciente, Table diagnosticos)
        {
            var listaDiagnosticos = diagnosticos.CreateSet<Diagnostico>().ToList();
            this._dniPaciente = dniPaciente;
            Paciente paciente = new Paciente(dniPaciente, "12345678912", new DateOnly(2002,03,26) , "rociopalma@gmail.com", "3873556746", "rocio", "palma", "av sarmiento", listaDiagnosticos); //Paciente simulado
            _clinicalRepository.GetPacienteByDni(dniPaciente).Returns(paciente);
        }

        [Given("el doctor escribe para el paciente previamente buscado un informe sobre el diagnostico {string} que dice {string}")]
        public void GivenElDoctorEscribeParaElPacientePreviamenteBuscadoUnInformeSobreElDiagnosticoQueDice(string diagnostico, string informe)
        {
            this._diagnosticoElegido = diagnostico;
            this._evolucionIngresada = new EvolucionRequestDTO{ Informe = informe };
        }

        [When("el medico guarda la evolucion")]
        public void WhenElMedicoGuardaLaEvolucion()
        {
            this._pacienteResultado = _clinicaService.AddEvolucionTextoLibre(_dniPaciente, _diagnosticoElegido, _medico, _evolucionIngresada);
        }

        [Then("se registra la evolucion en la historia clinica del paciente con el diagnostico, la descripcion y el medico.")]
        public void ThenSeRegistraLaEvolucionEnLaHistoriaClinicaDelPacienteConElDiagnosticoLaDescripcionYElMedico_()
        {
            Diagnostico? diagnosticoExist = _pacienteResultado.HistoriaClinica.GetDiagnostico(_diagnosticoElegido);
            Assert.True(diagnosticoExist?.ExistEvolucion(_medico, _evolucionIngresada.Informe));
            _clinicalRepository.Received(1).UpdatePaciente(_pacienteResultado);
        }
    }
}
