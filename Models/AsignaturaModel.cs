using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prestamosLibrosTFG.Models
{
    public class AsignaturaModel
    {

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string IdAsignatura { get; set; }

        [JsonProperty("nombre", NullValueHandling = NullValueHandling.Ignore)]
        public string Nombre { get; set; }

        public AsignaturaModel()
        {
            
        }
    }
}
