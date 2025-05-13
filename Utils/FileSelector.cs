using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pruebasProyecto.Utils
{
    public static class FileSelector
    {
        public static async Task<FileResult> SelectImageAsync()
        {
            try
            {
                var fileResult = await FilePicker.Default.PickAsync(new PickOptions
                {
                    PickerTitle = "Selecciona una imagen",
                    FileTypes = FilePickerFileType.Images
                });

                if (fileResult == null)
                    return null; // Usuario canceló la selección

                // Leer el archivo seleccionado como un stream
                using var stream = await fileResult.OpenReadAsync();

                // Redimensionar la imagen
                var resizedImage = ResizeImage(stream, 500); // Máximo de 500 px

                // Mantener el nombre original del archivo
                var originalFileName = Path.GetFileName(fileResult.FullPath);
                var tempFilePath = Path.Combine(FileSystem.CacheDirectory, originalFileName);

                // Guardar la imagen redimensionada en el directorio temporal
                File.WriteAllBytes(tempFilePath, resizedImage);

                return new FileResult(tempFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al seleccionar o procesar archivo: {ex.Message}");
                return null;
            }
        }

        private static byte[] ResizeImage(Stream imageStream, int maxDimension)
        {
            using var inputStream = new SKManagedStream(imageStream);
            using var original = SKBitmap.Decode(inputStream);

            // Calcular nuevas dimensiones manteniendo la relación de aspecto
            int width, height;
            if (original.Width > original.Height)
            {
                width = maxDimension;
                height = (original.Height * maxDimension) / original.Width;
            }
            else
            {
                height = maxDimension;
                width = (original.Width * maxDimension) / original.Height;
            }

            using var resized = original.Resize(new SKImageInfo(width, height), SKFilterQuality.High);
            if (resized == null)
                throw new Exception("Error al redimensionar la imagen.");

            using var image = SKImage.FromBitmap(resized);
            using var data = image.Encode(SKEncodedImageFormat.Jpeg, 90); // Calidad al 90%
            return data.ToArray();
        }

    }
}
