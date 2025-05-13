using Newtonsoft.Json;
using PersonasApp.Models;
using prestamosLibrosTFG.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PersonasApp.Services
{
    internal class APIService
    {

        // Método para ejecutar una solicitud HTTP y obtener una respuesta asincrónica
        public static async Task<ResponseModel> ExecuteRequest(RequestModel requestModel)
        {
            // Se crea una instancia de la clase ResponseModel para almacenar la respuesta
            ResponseModel responseModel = new ResponseModel();

            // Se serializa el objeto RequestModel a formato JSON
            var data = JsonConvert.SerializeObject(requestModel.Data);
            Debug.WriteLine(data);

            // Se utilizan bloques 'using' para asegurar que los recursos se liberen correctamente
            using (var handler = new StandardSocketsHttpHandler())
            using (var client = new HttpClient(handler))
            {

                // Se crea una nueva solicitud HTTP con el método y la ruta especificados en el objeto RequestModel
                var request = new HttpRequestMessage(new HttpMethod(requestModel.Method), requestModel.Route);

                // Se establece el encabezado 'Accept' para indicar que se acepta JSON como tipo de respuesta
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Se incluye el contenido JSON en la solicitud
                request.Content = new StringContent(data, Encoding.UTF8, "application/json");

                try
                {
                    // Se envía la solicitud al servidor de manera asíncrona y se espera una respuesta
                    using (HttpResponseMessage response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                    {
                        // Si la respuesta es exitosa
                        if (response.IsSuccessStatusCode)
                        {
                            // Se lee la respuesta como una cadena JSON
                            var stringResponse = await response.Content.ReadAsStringAsync();
                            if (stringResponse != null)
                            {
                                // Se deserializa la cadena JSON en un objeto ResponseModel
                                responseModel = JsonConvert.DeserializeObject<ResponseModel>(stringResponse) ?? new ResponseModel();

                                // Se imprime la respuesta en la consola de depuración
                                Debug.Write("Respuesta desde la API: ");
                                Debug.WriteLine(stringResponse);
                            }

                        }
                        else
                        {
                            // Si la respuesta no es exitosa, se imprime el código de estado en la consola de depuración
                            Debug.WriteLine(response.StatusCode);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Si se produce una excepción durante la solicitud, se imprime un mensaje de error en la consola de depuración
                    Debug.WriteLine($"Error al enviar la solicitud a la API: {ex.Message}");
                }
            }

            // Se devuelve el objeto ResponseModel, que contiene la respuesta de la API
            return responseModel;
        }
    }
}
