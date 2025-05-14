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
        private ObservableCollection<CursoModel> listaCursos;

        [ObservableProperty]
        private ObservableCollection<LibroModel> listaLibros;

        [ObservableProperty]
        private ObservableCollection<AsignaturaModel> listaAsignaturas;

        [ObservableProperty]
        private CursoModel selectedCurso;

        [ObservableProperty]
        private LibroModel selectedLibro;

        [ObservableProperty]
        private AsignaturaModel selectedAsignatura;

        [ObservableProperty]
        private LibroModel libro = new LibroModel();

        [ObservableProperty]
        public ObservableCollection<object> librosSeleccionados;

        [ObservableProperty]
        private ObservableCollection<LibroModel> listaLibrosFiltrada;

        public PaqueteViewModel()
        {
            ObtenerLibros();
            ObtenerCursos();

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

        //private async void ObtenerLibros()  // Metodo para obtener todos los libros
        //{
        //    RequestModel request = new RequestModel()
        //    {
        //        Method = "GET",
        //        Data = string.Empty,
        //        Route = "http://localhost:8080/libros/obtenerLibros"
        //    };

        //    ResponseModel response = await APIService.ExecuteRequest(request);
        //    if (response.Success.Equals(0))
        //    {
        //        try
        //        {
        //            ListaLibros =
        //                JsonConvert.DeserializeObject<ObservableCollection<LibroModel>>(response.Data.ToString());

        //        }
        //        catch (Exception ex)
        //        {
        //            Debug.Write(ex.StackTrace);
        //            Debug.Write(ex.ToString);
        //            Debug.Write(ex.Message);

        //        }
        //    }
        //}

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

            //OnPropertyChanged(nameof(Libro));
        }

        [RelayCommand] // Comando para llamar en el boton de limpiar
        public void Limpiar()
        {
            LimpiarCampos();
        }


    }


}
