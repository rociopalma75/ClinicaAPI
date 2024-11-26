Feature: Agregar nuevo diagnostico

Como medico
Quiero agregar un nuevo diagnostico
Para registrarlo en la historia clinica del paciente y poder ingresar una evolucion

@tag1
Scenario: El medico agrega un nuevo diagnostico a lista de diagnosticos del paciente
	Given el listado de diagnosticos del paciente con dni "44105560"
		| Id | Descripcion |
		| 1  | Angina      |
		| 2  | Dengue      |
		| 3  | Covid       |
	When el medico agrega el diagnostico "Coartacion Aortica"
	Then el diagnostico ingresado es incorporado en la lista de diagnosticos de la historia clinica del paciente.
