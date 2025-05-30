using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prestamosLibrosTFG.Models
{
    public class HacerPrestamoModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("matricula")]
        public MatriculaInfo Matricula { get; set; }

        [JsonProperty("paquete")]
        public PaqueteInfo1 Paquete { get; set; }

        [JsonProperty("fechaDevolucion")]
        public DateTime? FechaDevolucion { get; set; }

        [JsonProperty("devuelto")]
        public bool Devuelto { get; set; }
    }

    public class MatriculaInfo
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }

    public class PaqueteInfo1
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
