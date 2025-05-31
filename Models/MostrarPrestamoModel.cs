using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prestamosLibrosTFG.Models
{
    public class MostrarPrestamoModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("fechaPrestamo")]
        public DateTime? FechaPrestamo { get; set; }

        [JsonProperty("fechaDevolucion")]
        public DateTime? FechaDevolucion { get; set; }

        [JsonProperty("devuelto")]
        public bool Devuelto { get; set; }
        [JsonIgnore]
        public string DevueltoTexto => Devuelto ? "Sí" : "No";


        [JsonProperty("matricula")]
        public MatriculaInfoModel? Matricula { get; set; }

        [JsonProperty("paquete")]
        public PaqueteInfoModel? Paquete { get; set; }
    }

    public class MatriculaInfoModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("grupo")]
        public required string Grupo { get; set; }

        [JsonProperty("anioEscolar")]
        public required string AnioEscolar { get; set; }

        [JsonProperty("alumno")]
        public AlumnoInfoModel? Alumno { get; set; }

        [JsonProperty("curso")]
        public CursoInfoModel? Curso { get; set; }
    }

    public class AlumnoInfoModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("apellidos")]
        public string Apellidos { get; set; }
    }

    public class CursoInfoModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("nombreCurso")]
        public string NombreCurso { get; set; }
    }

    public class PaqueteInfoModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }
    }
}

