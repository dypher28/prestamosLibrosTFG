using prestamosLibrosTFG.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prestamosLibrosTFG.Converters
{
    public class ImagenNulaAImagenPorDefectoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var imagen = value as LibroModel.ImageInfo;
            return (imagen == null || string.IsNullOrEmpty(imagen.FileName))
                ? "librodefecto.png"
                : imagen.FileName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
