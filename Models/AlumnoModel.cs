using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prestamosLibrosTFG.Models
{
    
    public class AlumnoModel
    {

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int? IdAlumno { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("apellidos")]
        public string Apellidos { get; set; }
    }
}
