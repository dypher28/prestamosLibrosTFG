using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;

namespace prestamosLibrosTFG.Models
{
    [JsonObject]
    public class LibroModel : INotifyPropertyChanged
    {
        private ImageInfo _imagen;

        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("titulo")]
        public string Titulo { get; set; }

        [JsonProperty("editorial")]
        public string Editorial { get; set; }

        [JsonProperty("isbn")]
        public string Isbn { get; set; }

        [JsonProperty("cantidad")]
        public int? Cantidad { get; set; }

        [JsonProperty("asignatura")]
        public AsignaturaInfo1 Asignatura { get; set; }

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
                _imagen = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ImageSource));

                if (_imagen != null)
                {
                    _imagen.PropertyChanged += OnImagenPropertyChanged;
                }
            }
        }

        [JsonIgnore]
        public ImageSource ImageSource
        {
            get
            {
                var bytes = Imagen?.Data;
                if (bytes == null || bytes.Length == 0)
                    return null;
                return ImageSource.FromStream(() => new MemoryStream(bytes));
            }
        }

        private void OnImagenPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ImageInfo.Data))
            {
                OnPropertyChanged(nameof(ImageSource));
            }
        }

        [JsonObject]
        public class AsignaturaInfo1
        {
            [JsonProperty("id")]
            public int? Id { get; set; }

            [JsonProperty("nombre")]
            public string Nombre { get; set; }

            [JsonProperty("curso")]
            public CursoInfo1 Curso { get; set; }

            [JsonObject]
            public class CursoInfo1
            {
                [JsonProperty("id")]
                public int? Id { get; set; }

                [JsonProperty("nombreCurso")]
                public string NombreCurso { get; set; }
            }
        }

        [JsonObject]
        public class ImageInfo : INotifyPropertyChanged
        {
            [JsonProperty("id")]
            public int? Id { get; set; }

            private string _imagen;
            [JsonProperty("fileName")]
            public string FileName
            {
                get => _imagen;
                set
                {
                    if (_imagen != value)
                    {
                        _imagen = value;
                        OnPropertyChanged();
                    }
                }
            }

            [JsonProperty("contentType")]
            public string ContentType { get; set; }

            private byte[] _data;
            [JsonProperty("data")]
            public byte[] Data
            {
                get => _data;
                set
                {
                    if (_data != value)
                    {
                        _data = value;
                        OnPropertyChanged();
                    }
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}