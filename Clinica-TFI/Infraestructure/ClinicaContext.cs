using Clinica_TFI.Domain;

namespace Clinica_TFI.Infraestructure
{
    public class ClinicaContext
    {
        public List<Paciente> Pacientes { get; set; }
        public List<Medico> Medicos { get; set; }
        public List<CatalogoPlantillas> CatalogoPlantillas {  get; set; }
        public ClinicaContext()
        {
            Pacientes = new List<Paciente>
            {
                new Paciente("44105666", "20-44105666-3", new DateOnly(1990, 5, 15), "juan.perez@example.com", "+54 9 351 123-4567", "Juan", "Pérez", "Calle Falsa 123, Córdoba"),
                new Paciente("40235478", "20-40235478-7", new DateOnly(1985, 3, 22), "maria.gomez@example.com", "+54 9 261 987-6543", "María", "Gómez", "Av. Siempre Viva 742, Mendoza"),
                new Paciente("35462133", "20-35462133-5", new DateOnly(1995, 12, 10), "carlos.lopez@example.com", "+54 9 11 345-6789", "Carlos", "López", "Calle Los Álamos 567, Buenos Aires"),
                new Paciente("37985641", "20-37985641-6", new DateOnly(2000, 7, 3), "sofia.martinez@example.com", "+54 9 341 234-5678", "Sofía", "Martínez", "Pasaje Las Rosas 112, Rosario"),
                new Paciente("42358796", "20-42358796-8", new DateOnly(1993, 10, 18), "lucia.fernandez@example.com", "+54 9 261 654-3210", "Lucía", "Fernández", "Boulevard Central 456, Mendoza")
            };

            Medicos = new List<Medico>()
            {
                new Medico("Rocio", "Palma", "12345/Cl", "Clinico", "rociopalma@gmail.com", "admin")
            };
            List<string> campos = new List<string>
            {
                "Altura",
                "Peso",
                "Presion Arterial"
            };

            List<string> camposPlantilla2 = new List<string>
            {
                "Temperatura",
                "Pulso"
            };

            CatalogoPlantillas = new List<CatalogoPlantillas>() 
            { 
                new CatalogoPlantillas(1,campos),
                new CatalogoPlantillas(2, camposPlantilla2)
            } ;
        }

    }
}
