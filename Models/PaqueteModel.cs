using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace prestamosLibrosTFG.Models
{
    [JsonObject]
    public class PaqueteModel
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("curso")]
        public Curso Curso { get; set; }

        [JsonProperty("libros")]
        public HashSet<LibroInfoModel> Libros { get; set; } = new();

        [JsonProperty("prestamos")]
        public HashSet<PrestamoModel> Prestamos { get; set; } = new();

        // Constructor
        public PaqueteModel() { }

        public PaqueteModel(int id, string nombre, Curso curso)
        {
            Id = id;
            Nombre = nombre;
            Curso = curso;
        }
    }

        // Clase anidada Curso
        public class Curso
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("nombreCurso")]
            public string Nombre { get; set; }
        }

        // Clase anidada Libro
        public class LibroInfoModel
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("titulo")]
            public string Titulo { get; set; }

            [JsonProperty("editorial")]
            public string Editorial { get; set; }

            [JsonProperty("cantidad")]
            public int Cantidad { get; set; }

            [JsonProperty("isbn")]
            public string Isbn { get; set; }
        }

        // Clase anidada Prestamo
        public class PrestamoModel
        {
            [JsonProperty("id_prestamo")]
            public int Id { get; set; }

            [JsonProperty("fechaPrestamo")]
            public DateTime? FechaPrestamo { get; set; }
        }
    
}
