using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonasApp.Services
{
    class UploadImageService
    {
        public static async Task<string> UploadImageAsync(string filePath, string idPersona, string url)
        {
            try
            {
                byte[] fileBytes = File.ReadAllBytes(filePath);

                using (HttpClient client = new HttpClient())
                using (var content = new MultipartFormDataContent())
                {
                    var fileContent = new ByteArrayContent(fileBytes);
                    fileContent.Headers.Add("Content-Type", "application/octet-stream");
                    content.Add(fileContent, "file", idPersona + Path.GetExtension(filePath));

                    // Enviar la imagen al servidor de Spring Boot
                    HttpResponseMessage response = await client.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        Debug.WriteLine("Imagen subida correctamente.");
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        Debug.WriteLine($"Error al subir la imagen. Código de estado: {response.StatusCode}");
                        return null;  // Devuelves null directamente
                    }
                }
            }
            catch (Exception ex)
            {
                // En caso de error en la lectura del archivo o la solicitud HTTP
                Debug.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

    }
}
