using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prestamosLibrosTFG.Models
{
    public class CursoModel
    {

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string IdCurso { get; set; }

        [JsonProperty("nombreCurso", NullValueHandling = NullValueHandling.Ignore)]
        public string Curso { get; set; }

        [JsonProperty("libros")]
        public ObservableCollection<LibroModel> Libros { get; set; }

        public CursoModel()
        {

        }

    }
}
