using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prestamosLibrosTFG.Models
{
    public class MatriculaModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("grupo")]
        public string Grupo { get; set; }

        [JsonProperty("anioEscolar")]
        public string AnioEscolar { get; set; }

        [JsonProperty("alumno")]
        public AlumnoInfo Alumno { get; set; }

        [JsonProperty("curso")]
        public CursoInfo Curso { get; set; }

        [JsonProperty("prestamos")]
        public List<PrestamoInfo> Prestamos { get; set; }
    }

    public class AlumnoInfo
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("apellidos")]
        public string Apellidos { get; set; }
    }

    public class CursoInfo
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("nombreCurso")]
        public string NombreCurso { get; set; }
    }

    public class PrestamoInfo
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("fechaPrestamo")]
        public DateTime? FechaPrestamo { get; set; }

        [JsonProperty("fechaDevolucion")]
        public DateTime? FechaDevolucion { get; set; }

        [JsonProperty("devuelto")]
        public bool Devuelto { get; set; }

        [JsonProperty("paquete")]
        public PaqueteInfo Paquete { get; set; }
    }

    public class PaqueteInfo
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }
    }
}
