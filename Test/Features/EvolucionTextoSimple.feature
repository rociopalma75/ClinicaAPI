Feature: Agregar una nueva evolucion con diagnostico previo

Para que se pueda diagnosticar al paciente
el medico
quiere añadir una nueva evolucion en la historia clinica del paciente eligiendo un diagnostico previo.

Background: El medico visualiza una historia clinica del paciente.
	Given el medico "Dr Rocio Palma" ha iniciado sesion.
	And ha buscado la historia clinica del paciente "44105560" que posee los siguientes diagnosticos
	| Id | Descripcion |
	| 1  | Angina      |
	| 2  | Dengue      |
	| 3  | Covid       |

@tag1
Scenario: El medico agrega una evolucion con texto libre
	Given el doctor escribe para el paciente previamente buscado un informe sobre el diagnostico "Angina" que dice "El paciente presenta los sintomas de una angina"
	When el medico guarda la evolucion
	Then se registra la evolucion en la historia clinica del paciente con el diagnostico, la descripcion y el medico.