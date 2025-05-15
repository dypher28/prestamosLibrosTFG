using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using PersonasApp.Models;
using PersonasApp.Services;
using prestamosLibrosTFG.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prestamosLibrosTFG.ViewModels
{
    public partial class PaqueteViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isCursoChecked = false;

        [ObservableProperty]
        private ObservableCollection<CursoModel> listaCursos; // Lista del picker para los filtros

        [ObservableProperty]
        private ObservableCollection<CursoModel> listaCursosPaquete; // Lista del picker para crear el paquete

        [ObservableProperty]
        private CursoModel selectedCursoPaquete; // SelectedLibro para crear el paquete

        [ObservableProperty]
        private PaqueteModel paquete;

        [ObservableProperty]
        private ObservableCollection<LibroModel> listaLibros;

        [ObservableProperty]
        private ObservableCollection<AsignaturaModel> listaAsignaturas;

        [ObservableProperty]
        private CursoModel selectedCurso; // SelectedLibro para los filtros

        [ObservableProperty]
        private AsignaturaModel selectedAsignatura;

        [ObservableProperty]
        private LibroModel libro = new();

        [ObservableProperty]
        private ObservableCollection<object> librosSeleccionados;

        [ObservableProperty]
        private ObservableCollection<LibroModel> listaLibrosFiltrada;

        public PaqueteViewModel()
        {
            Paquete = new PaqueteModel();
            OnPropertyChanged(nameof(Paquete));
            LibrosSeleccionados = new ObservableCollection<object>();
            ObtenerLibros();
            ObtenerCursos();
            ObtenerCursosPaquete();

        }


        [RelayCommand] // Metodo para obtener los cursos
        public async Task ObtenerCursos()
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
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.StackTrace);
                    Debug.Write(ex.ToString);
                    Debug.Write(ex.Message);

                }
            }
        }

        [RelayCommand] // Metodo para obtener los cursos del picker de crear paquete
        public async Task ObtenerCursosPaquete()
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
                    ListaCursosPaquete =
                        JsonConvert.DeserializeObject<ObservableCollection<CursoModel>>(response.Data.ToString());
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.StackTrace);
                    Debug.Write(ex.ToString);
                    Debug.Write(ex.Message);

                }
            }
        }

        private async void ObtenerLibros()
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
                    var libros = JsonConvert.DeserializeObject<ObservableCollection<LibroModel>>(response.Data.ToString());

                    ListaLibros = libros;
                    ListaLibrosFiltrada = new ObservableCollection<LibroModel>(libros);

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
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.StackTrace);
                    Debug.Write(ex.ToString);
                    Debug.Write(ex.Message);

                }
            }
        }

        // Metodos para filtrar en tiempo real
        partial void OnLibroChanged(LibroModel value)
        {
            FiltrarLibros();
        }

        partial void OnSelectedCursoChanged(CursoModel value)
        {
            ObtenerAsignaturasCommand.Execute(null);
            FiltrarLibros();
        }

        partial void OnSelectedAsignaturaChanged(AsignaturaModel value)
        {
            FiltrarLibros();
        }

        [RelayCommand]
        private void FiltrarLibros()
        {
            if (ListaLibros == null) return;

            var librosFiltrados = ListaLibros.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(Libro?.Isbn))
                librosFiltrados = librosFiltrados.Where(l =>
                    !string.IsNullOrWhiteSpace(l.Isbn) &&
                    l.Isbn.Contains(Libro.Isbn, StringComparison.OrdinalIgnoreCase));

            if (SelectedCurso?.IdCurso != null)
                librosFiltrados = librosFiltrados.Where(l =>
                    l.Asignatura?.Curso?.Id == int.Parse(SelectedCurso.IdCurso));

            if (SelectedAsignatura?.IdAsignatura != null)
                librosFiltrados = librosFiltrados.Where(l =>
                    l.Asignatura?.Id == int.Parse(SelectedAsignatura.IdAsignatura));

            ListaLibrosFiltrada = new ObservableCollection<LibroModel>(librosFiltrados);
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
        }

        [RelayCommand] // Comando para llamar en el boton de limpiar
        public void Limpiar()
        {
            LimpiarCampos();
        }

        private async Task MostrarMensaje(string mensaje)
        {
            await App.Current.MainPage.DisplayAlert("Aviso", mensaje, "OK");
        }


        [RelayCommand]
        public async void CrearPaquete()
        {
            if (string.IsNullOrWhiteSpace(Paquete?.Nombre))
            {
                await MostrarMensaje("Debes ingresar un nombre para el paquete.");
                return;
            }

            if (SelectedCursoPaquete == null)
            {
                await MostrarMensaje("Debes seleccionar un curso.");
                return;
            }

            if (LibrosSeleccionados == null || LibrosSeleccionados.Count == 0)
            {
                await MostrarMensaje("Debes seleccionar al menos un libro.");
                return;
            }

            //// Asignar el curso desde el modelo de selección
            //Paquete.Curso = new Curso
            //{
            //    Id = SelectedCursoPaquete.IdCurso,
            //    Nombre = SelectedCursoPaquete.Curso
            //};

            //// Asignar los libros seleccionados al paquete
            //Paquete.Libros = LibrosSeleccionados
            //    .Select(libro => new LibroInfo
            //    {
            //        Id = libro.Id,
            //        Titulo = libro.Titulo
            //    }).ToHashSet();


            // Crear y enviar la solicitud
            var request = new RequestModel
            {
                Method = "POST",
                Route = "http://localhost:8080/paquetes/crearPaquete",
                Data = Paquete
            };

            var response = await APIService.ExecuteRequest(request);
            if (response.Success == 0)
            {
                await MostrarMensaje("Paquete creado correctamente.");

                // Limpiar campos
                Paquete = new PaqueteModel();
                SelectedCursoPaquete = null;
                LibrosSeleccionados.Clear();
            }
            else
            {
                await MostrarMensaje("Error al crear el paquete. Inténtalo de nuevo.");
            }
        }

    }
}
