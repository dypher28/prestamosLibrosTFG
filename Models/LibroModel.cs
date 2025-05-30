using Newtonsoft.Json;
using System;
using System.IO;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace prestamosLibrosTFG.Models
{
    [JsonObject]
    public class LibroModel : ObservableObject
    {
        private ImageInfo _imagen;

        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("titulo")]
        public string? Titulo { get; set; }

        [JsonProperty("editorial")]
        public string? Editorial { get; set; }

        [JsonProperty("isbn")]
        public string? Isbn { get; set; }

        [JsonProperty("cantidad")]
        public int? Cantidad { get; set; }

        [JsonProperty("asignatura")]
        public AsignaturaInfo Asignatura { get; set; }

        /// <summary>
        /// Propiedad que contiene la información de la imagen.
        /// Al asignarla, nos suscribimos/desuscribimos para detectar cambios en ImageInfo.Data.
        /// </summary>
        [JsonProperty("image")]
        public ImageInfo Imagen
        {
            get => _imagen;
            set
            {
                if (_imagen != null)
                {
                    _imagen.PropertyChanged -= OnImagenPropertyChanged;
                }

                SetProperty(ref _imagen, value);
                OnPropertyChanged(nameof(ImageSource));

                if (_imagen != null)
                {
                    _imagen.PropertyChanged += OnImagenPropertyChanged;
                }
            }
        }

        /// <summary>
        /// Convierte los bytes de la imagen (ImageInfo.Data) en un ImageSource de MAUI.
        /// Si no hay datos, cargará una imagen por defecto llamada "librodefecto.png".
        /// </summary>
        [JsonIgnore]
        public ImageSource ImageSource
        {
            get
            {
                var bytes = Imagen?.Data;
                if (bytes == null || bytes.Length == 0)
                {
                    return ImageSource.FromFile("librodefecto.png");
                }

                return ImageSource.FromStream(() => new MemoryStream(bytes));
            }
        }

        /// <summary>
        /// Cada vez que ImageInfo.Data cambie, notificamos que ImageSource ha cambiado.
        /// </summary>
        private void OnImagenPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ImageInfo.Data))
            {
                OnPropertyChanged(nameof(ImageSource));
            }
        }


        #region Clases Anidadas

        /// <summary>
        /// Representa la asignatura a la que pertenece el libro.
        /// </summary>
        [JsonObject]
        public class AsignaturaInfo : ObservableObject
        {
            [JsonProperty("id")]
            public int? Id { get; set; }

            private string _nombre = string.Empty;
            [JsonProperty("nombre")]
            public string Nombre
            {
                get => _nombre;
                set => SetProperty(ref _nombre, value);
            }

            [JsonProperty("curso")]
            public CursoInfo Curso { get; set; }

            /// <summary>
            /// Información del curso dentro de la asignatura.
            /// </summary>
            [JsonObject]
            public class CursoInfo : ObservableObject
            {
                [JsonProperty("id")]
                public int? Id { get; set; }

                private string _nombreCurso = string.Empty;
                [JsonProperty("nombreCurso")]
                public string NombreCurso
                {
                    get => _nombreCurso;
                    set => SetProperty(ref _nombreCurso, value);
                }
            }
        }

        /// <summary>
        /// Contiene todos los datos de la imagen: Id, FileName, ContentType y Data (bytes).
        /// Al cambiar "Data", dispara notificación para que la UI actualice ImageSource en LibroModel.
        /// </summary>
        [JsonObject]
        public class ImageInfo : ObservableObject
        {
            [JsonProperty("id")]
            public int? Id { get; set; }

            private string _fileName = string.Empty;
            [JsonProperty("fileName")]
            public string FileName
            {
                get => _fileName;
                set => SetProperty(ref _fileName, value);
            }

            private string _contentType = string.Empty;
            [JsonProperty("contentType")]
            public string ContentType
            {
                get => _contentType;
                set => SetProperty(ref _contentType, value);
            }

            private byte[] _data = Array.Empty<byte>();
            [JsonProperty("data")]
            public byte[] Data
            {
                get => _data;
                set => SetProperty(ref _data, value);
            }
        }

        #endregion
    }
}
