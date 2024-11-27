using System.Text.Json;

namespace Clinica_TFI.Domain
{
    public class CatalogoPlantillas
    {
        public int Id { get; set; }
        public List<string> Campos { get; set; }

        public CatalogoPlantillas(int id)
        {
            Campos = new List<string>();
            Id = id;
        }

        public CatalogoPlantillas(int id, List<string> campos)
        {
            Id = id;
            Campos = campos;
        }

        public bool VerificarPropiedades(dynamic plantilla)
        {
            var properties = new List<string>();

            foreach(JsonProperty jsonProperty in plantilla.EnumerateObject())
            {
                properties.Add(jsonProperty.Name);
            }

            if (properties.Count != this.Campos.Count) return false;

            foreach(var property in properties)
            {
                bool exist = this.Campos.Any(c=> c.ToLower() == property.ToLower()); 
                if (!exist) return false;
            }
            return true;
        }
    }
}
