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
        [JsonProperty("id_paquete")]
        public int Id { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("curso_id")]
        public Curso Curso { get; set; }

        [JsonProperty("libros")]
        public HashSet<Libro> Libros { get; set; } = new HashSet<Libro>();

        [JsonProperty("prestamos")]
        public HashSet<Prestamo> Prestamos { get; set; } = new HashSet<Prestamo>();

        // Constructor
        public PaqueteModel() { }

        public PaqueteModel(int id, string nombre, Curso curso)
        {
            Id = id;
            Nombre = nombre;
            Curso = curso;
        }
    }

    public class Curso
    {
        [JsonProperty("id_curso")]
        public int Id { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }
    }

    public class Libro
    {
        [JsonProperty("id_libro")]
        public int Id { get; set; }

        [JsonProperty("titulo")]
        public string Titulo { get; set; }
    }

    public class Prestamo
    {
        [JsonProperty("id_prestamo")]
        public int Id { get; set; }

        [JsonProperty("fecha_prestamo")]
        public DateTime FechaPrestamo { get; set; }
    }
}
