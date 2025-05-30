using prestamosLibrosTFG.Models;
using System;
using System.Globalization;
using System.IO;
using Microsoft.Maui.Controls;

namespace prestamosLibrosTFG.Converters
{
    public class ImagenNulaAImagenPorDefectoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // “value” ya es un LibroModel.ImageInfo
            var imagen = value as LibroModel.ImageInfo;

            // 1) Si no hay instancia o el FileName está vacío, devolvemos el fallback
            if (imagen == null || string.IsNullOrWhiteSpace(imagen.FileName))
            {
                return ImageSource.FromFile("librodefecto.png");
            }

            // 2) Si era el nombre “librodefecto.png” por alguna razón, devolvemos la misma
            if (imagen.FileName.Equals("librodefecto.png", StringComparison.OrdinalIgnoreCase))
            {
                return ImageSource.FromFile("librodefecto.png");
            }

            // 3) Si hay datos (byte[]) para la imagen, la devolvemos desde un Stream
            if (imagen.Data != null && imagen.Data.Length > 0)
            {
                return ImageSource.FromStream(() => new MemoryStream(imagen.Data));
            }

            // 4) Si no tenía bytes pero sí FileName (que podría ser ruta física),
            //    cargamos la imagen a partir de la ruta (por ejemplo, si FileName = ruta en disco)
            try
            {
                // Intentamos leer directamente del fichero físico
                if (File.Exists(imagen.FileName))
                {
                    var bytes = File.ReadAllBytes(imagen.FileName);
                    return ImageSource.FromStream(() => new MemoryStream(bytes));
                }
            }
            catch
            {
                // ignoramos excepciones de I/O
            }

            // 5) En cualquier otro caso devolvemos el fallback
            return ImageSource.FromFile("librodefecto.png");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
