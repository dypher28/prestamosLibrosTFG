using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prestamosLibrosTFG.Converters
{
    public class AsignaturaColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string nombreAsignatura = value as string;

            return nombreAsignatura switch
            {
                string s when s.Contains("Matemáticas") => Colors.LightBlue,
                string s when s.Contains("Matematicas") => Colors.LightBlue,
                string s when s.Contains("Lengua Castellana") => Colors.LightPink,
                string s when s.Contains("Ciencias Sociales") => Colors.BurlyWood,
                string s when s.Contains("Ciencias Naturales") => Colors.LightGreen,
                string s when s.Contains("Conocimiento del medio") => Colors.LightGreen,
                string s when s.Contains("Inglés") => Colors.Purple,
                string s when s.Contains("Ingles") => Colors.Purple,
                string s when s.Contains("Educación Física") => Colors.BlueViolet,
                string s when s.Contains("Religión") => Colors.Purple,
                string s when s.Contains("Religion") => Colors.Purple,
                string s when s.Contains("Sociales") => Colors.CornflowerBlue,
                string s when s.Contains("Plástica") => Colors.LightCyan,
                string s when s.Contains("Plastica") => Colors.LightCyan,
                string s when s.Contains("Música") => Colors.LightSalmon,
                _ => Colors.LightGray // Color por defecto
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
