using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using PersonasApp.Models;
using PersonasApp.Services;
using prestamosLibrosTFG.Models;
using pruebasProyecto.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace prestamosLibrosTFG.ViewModels
{
    [QueryProperty(nameof(Libro), "Libro")]
    public partial class FormViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isCursoChecked = false;

        [ObservableProperty]
        private LibroModel libro;

        [ObservableProperty]
        private string rutaImagen = "librodefecto.png";

        [ObservableProperty]
        private ObservableCollection<CursoModel> listaCursos;

        [ObservableProperty]
        private ObservableCollection<AsignaturaModel> listaAsignaturas;

        [ObservableProperty]
        private ObservableCollection<LibroModel> listaLibros;

        [ObservableProperty]
        private CursoModel selectedCurso;

        [ObservableProperty]
        private AsignaturaModel selectedAsignatura;

        [ObservableProperty]
        public string cantidad = "1";


        public FormViewModel()
        {
            //ObtenerCursos();
            Libro = new LibroModel();
            Libro.Imagen = new LibroModel.ImageInfo();
            Libro.Imagen.FileName = RutaImagen;
        }


        [RelayCommand] // Metodo para seleccionar la imagen del libro (portada)
        public async void SeleccionarImagen()
        {
            var file = await FileSelector.SelectImageAsync();
            if (file != null)
            {
                RutaImagen = file.FullPath;
                Debug.WriteLine($"Ruta de la imagen seleccionada: {RutaImagen}");

                try
                {
                    Libro.Imagen.FileName = RutaImagen;
                    var bytes = File.ReadAllBytes(RutaImagen);
                    Libro.Imagen.Data = bytes;
                    OnPropertyChanged(nameof(Libro));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error al asignar la imagen: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("No se seleccionó ninguna imagen");
            }

        }
        [RelayCommand]
        public async Task ObtenerCursos() // Metodo para obtener todos los cursos
        {
            RequestModel request = new RequestModel()
            {
                Method = "GET",
                Data = string.Empty,
                Route = "http://localhost:8080/cursos/obtenerCursos"
            };

            ResponseModel response = await APIService.ExecuteRequest(request);
            if (response.Success.Equals(0))
            {
                try
                {
                    ListaCursos =
                        JsonConvert.DeserializeObject<ObservableCollection<CursoModel>>(response.Data.ToString());

                    if (Libro?.Id != null && Libro.Id != 0)
                    {
                        var cursoId = Libro.Asignatura?.Curso?.Id?.ToString();
                        if (!string.IsNullOrEmpty(cursoId))
                        {
                            SelectedCurso = ListaCursos.FirstOrDefault(c => c.IdCurso == cursoId);
                        }

                    }
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.StackTrace);
                    Debug.Write(ex.ToString);
                    Debug.Write(ex.Message);

                }
            }
        }

        [RelayCommand] // Metodo para obtener todas las asignaturas de un curso
        public async Task ObtenerAsignaturas()
        {
            if (SelectedCurso == null)
            {
                return;
            }
            RequestModel request = new RequestModel()
            {
                Method = "GET",
                Data = string.Empty,
                Route = "http://localhost:8080/cursos/asignaturasCurso/" + SelectedCurso.IdCurso
            };

            ResponseModel response = await APIService.ExecuteRequest(request);
            if (response.Success.Equals(0))
            {
                try
                {
                    ListaAsignaturas =
                        JsonConvert.DeserializeObject<ObservableCollection<AsignaturaModel>>(response.Data.ToString());

                    var asignaturaId = Libro.Asignatura?.Id?.ToString();
                    if (!string.IsNullOrEmpty(asignaturaId))
                    {
                        SelectedAsignatura = ListaAsignaturas.FirstOrDefault(a => a.IdAsignatura == asignaturaId);
                    }
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.StackTrace);
                    Debug.Write(ex.ToString);
                    Debug.Write(ex.Message);

                }
            }
        }

        [RelayCommand] // Metodo para obtener todos los libros
        public async Task ObtenerLibros()
        {
            RequestModel request = new RequestModel()
            {
                Method = "GET",
                Data = string.Empty,
                Route = "http://localhost:8080/libros/obtenerLibros"
            };

            ResponseModel response = await APIService.ExecuteRequest(request);
            if (response.Success.Equals(0))
            {
                try
                {
                    ListaLibros =
                        JsonConvert.DeserializeObject<ObservableCollection<LibroModel>>(response.Data.ToString());
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.StackTrace);
                    Debug.Write(ex.ToString);
                    Debug.Write(ex.Message);

                }
            }
        }

        // Método para comprobar si un ISBN ya existe en la base de datos
        private async Task<bool> ExisteISBN(string isbn)
        {
            var request = new RequestModel
            {
                Method = "GET",
                Route = $"http://localhost:8080/libros/existeISBN/{isbn}"
            };

            var response = await APIService.ExecuteRequest(request);
            return response.Data is true;
        }

        private async Task MostrarMensaje(string mensaje)
        {
            await App.Current.MainPage.DisplayAlert("Aviso", mensaje, "OK");
        }


        [RelayCommand] // Metodo para registrar un libro
        public async void CrearLibro()
        {
            Debug.WriteLine("VAS A CREAR UN LIBRO");

            if (SelectedCurso == null)
            {
                await MostrarMensaje("Debes seleccionar un curso.");
                return;
            }

            if (SelectedAsignatura == null)
            {
                await MostrarMensaje("Debes seleccionar una asignatura.");
                return;
            }

            if (string.IsNullOrWhiteSpace(Libro?.Titulo) ||
                string.IsNullOrWhiteSpace(Libro?.Editorial) ||
                string.IsNullOrWhiteSpace(Libro?.Isbn))
            {
                await MostrarMensaje("Por favor, completa todos los campos");
                return;
            }

            if (Libro.Id == null || Libro.Id == 0)
            {
                if (await ExisteISBN(Libro.Isbn))
                {
                    await MostrarMensaje("Ya existe un libro con ese ISBN.");
                    return;
                }
            }


            if (Libro.Asignatura == null)
                Libro.Asignatura = new LibroModel.AsignaturaInfo();

            if (Libro.Asignatura.Curso == null)
                Libro.Asignatura.Curso = new LibroModel.AsignaturaInfo.CursoInfo();

            Libro.Asignatura.Curso.Id = int.Parse(SelectedCurso.IdCurso);
            Libro.Asignatura.Id = int.Parse(SelectedAsignatura.IdAsignatura);
            Libro.Cantidad = int.Parse(Cantidad);

            // Si se seleccionó una imagen personalizada, subirla
            if (!string.IsNullOrEmpty(RutaImagen) && File.Exists(RutaImagen) && !RutaImagen.Contains("librodefecto.png"))
            {
                var client = new HttpClient();
                using var content = new MultipartFormDataContent();

                var imageContent = new StreamContent(new FileStream(RutaImagen, FileMode.Open, FileAccess.Read));
                imageContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                content.Add(imageContent, "file", Path.GetFileName(RutaImagen));

                var response_img = await client.PostAsync("http://localhost:8080/api/images", content);
                response_img.EnsureSuccessStatusCode();
                var id = await response_img.Content.ReadFromJsonAsync<int>();
                Libro.Imagen.Id = id;
                Libro.Imagen.FileName = Path.GetFileName(RutaImagen);
                RutaImagen = "librodefecto.png";
            }
            else
            {

                Libro.Imagen = new LibroModel.ImageInfo();
                Libro.Imagen.Id = 0;


            }

            var request = new RequestModel
            {
                Method = "POST",
                Route = "http://localhost:8080/libros/crearLibro",
                Data = Libro
            };

            var response = await APIService.ExecuteRequest(request);
            if (response.Success == 0)
            {
                await MostrarMensaje("Libro creado correctamente.");
                await ObtenerLibros();
                LimpiarCampos();

            }
            else
            {
                await MostrarMensaje("Error al crear el libro. Inténtalo de nuevo.");
            }
        }


        private void LimpiarCampos() // Metodo para limpiar los campos del formulario
        {
            Libro = new LibroModel
            {
                Imagen = new LibroModel.ImageInfo
                {
                    FileName = "librodefecto.png"
                }
            };

            SelectedCurso = null;
            SelectedAsignatura = null;
            Cantidad = "1";
        }

        [RelayCommand] // Comando para llamar en el boton de limpiar
        public void LimpiarFormulario()
        {
            LimpiarCampos();
        }

    }
}
