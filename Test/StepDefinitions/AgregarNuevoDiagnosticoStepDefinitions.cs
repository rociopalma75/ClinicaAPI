using Clinica_TFI.Domain.Contracts;
using System;
using TechTalk.SpecFlow;
using NSubstitute;
using AutoMapper;
using Clinica_TFI.Application;
using Clinica_TFI.Domain;
using TechTalk.SpecFlow.Assist;
using Clinica_TFI.Application.DTO;

namespace Test.StepDefinitions
{

    [Binding]
    public class AgregarNuevoDiagnosticoStepDefinitions
    {
        private readonly IClinicaRepository _clinicaRepository = Substitute.For<IClinicaRepository>();
        private readonly IMapper _mapper = Substitute.For<IMapper>();
        private ClinicaService _clinicaService;
        private Paciente _pacienteResultado = null!;
        private string _dníPaciente = null!;
        private DiagnosticoRequestDTO _diagnosticoNuevo = null!;

        [Before]
        public void Setup()
        {
            _pacienteResultado = null;
            _dníPaciente = null;
            _diagnosticoNuevo = null!;
            _clinicaService = new ClinicaService(_clinicaRepository, _mapper);
        }


        [Given("el listado de diagnosticos del paciente con dni {string}")]
        public void GivenElListadoDeDiagnosticosDelPacienteConDni(string dniPaciente, Table diagnosticos)
        {
            this._dníPaciente = dniPaciente;
            var listaDiagnosticos = diagnosticos.CreateSet<Diagnostico>().ToList();
            Paciente paciente = new Paciente(dniPaciente, "12345678912", new DateOnly(2002, 03, 26), "rociopalma@gmail.com", "3873556746", "rocio", "palma", "av sarmiento", listaDiagnosticos); //Paciente simulado
            _clinicaRepository.GetPacienteByDni(dniPaciente).Returns(paciente);
        }

        [When("el medico agrega el diagnostico {string}")]
        public void WhenElMedicoAgregaElDiagnostico(string nombreDiagnostico)
        {
            _diagnosticoNuevo = new DiagnosticoRequestDTO { Descripcion = nombreDiagnostico};
            _pacienteResultado = _clinicaService.AddDiagnostico(_diagnosticoNuevo, _dníPaciente);
        }

        [Then("el diagnostico ingresado es incorporado en la lista de diagnosticos de la historia clinica del paciente.")]
        public void ThenElDiagnosticoIngresadoEsIncorporadoEnLaListaDeDiagnosticosDeLaHistoriaClinicaDelPaciente_()
        {
            bool verify = _pacienteResultado.HistoriaClinica.ExistDiagnostico(_diagnosticoNuevo.Descripcion);
            Assert.True(verify);
            _clinicaRepository.Received(1).UpdatePaciente(_pacienteResultado);
        }
    }
}
