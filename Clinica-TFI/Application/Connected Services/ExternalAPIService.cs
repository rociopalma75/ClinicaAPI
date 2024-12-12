using Clinica_TFI.Domain;
using System.Net.Http;
using System.Text.Json;

namespace Clinica_TFI.Application.Connected_Services
{
    public class ExternalAPIService
    {
        private readonly string _url = "https://istp1service.azurewebsites.net/api/servicio-salud/medicamentos";
        private readonly string _urlObraSocial = "https://istp1service.azurewebsites.net/api/servicio-salud/obras-sociales";
        public async Task<List<Medicamento>> GetMedicamentosByCodigos(List<int> codigos)
        {
            List<Medicamento> resultsMedicamentos = new List<Medicamento>();

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var tasks = codigos.Select(async codigo =>
                    {
                        var response = await httpClient.GetAsync($"{_url}/{codigo}");

                        if (response.IsSuccessStatusCode)
                        {
                            var contenido = await response.Content.ReadAsStringAsync();
                            return JsonSerializer.Deserialize<Medicamento>(contenido, new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            });
                        }
                        else
                        {
                            throw new Exception($"No se encontró el medicamento con el código {codigo}");
                        }
                    });
                    // Espera a que todas las tareas se completen
                    var resultados = await Task.WhenAll(tasks);

                    // Agrega los resultados al listado final
                    resultsMedicamentos.AddRange(resultados);

                }
                catch (Exception ex) { throw new Exception(ex.Message); }
            }

            return resultsMedicamentos;
        }

        public async Task<ObraSocial> GetObraSocial(int codigo)
        { 
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetAsync($"{_url}/{codigo}");

                    if (response.IsSuccessStatusCode)
                    {
                        var contenido = await response.Content.ReadAsStringAsync();
                        var obraSocial  = JsonSerializer.Deserialize<ObraSocial>(contenido, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        }) ;

                        if (obraSocial == null)
                        {
                            throw new Exception($"La deserialización de la respuesta para el código {codigo} devolvió null.");
                        }

                        return obraSocial;
                    }
                    else
                    {
                        throw new Exception($"No se encontró la obra social con el código {codigo}");
                    }

                }
                catch (Exception ex) { throw new Exception(ex.Message); }
            }
        }
    }
}
