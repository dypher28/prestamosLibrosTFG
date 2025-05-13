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
    public partial class MainViewModel: ObservableObject
    {

        [ObservableProperty]
        private ObservableCollection<CursoModel> listaCursos;

        [ObservableProperty]
        private ObservableCollection<LibroModel> listaLibros;

        [ObservableProperty]
        private ObservableCollection<AsignaturaModel> listaAsignaturas;

        [ObservableProperty]
        private CursoModel selectedCurso;

        [ObservableProperty]
        private AsignaturaModel selectedAsignatura;

        [ObservableProperty]
        private LibroModel selectedLibro;

        [ObservableProperty]
        private bool isCursosVisible;

        [ObservableProperty]
        private bool isAsignaturasVisible;

        [ObservableProperty]
        private bool isLibrosVisible;

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

                    IsCursosVisible = true;
                    IsAsignaturasVisible = false;
                    IsLibrosVisible = false;
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.StackTrace);
                    Debug.Write(ex.ToString);
                    Debug.Write(ex.Message);

                }
            }
        }

        [RelayCommand]
        public async Task ObtenerAsignaturas() // Metodo para obtener las asignaturas de un curso
        {
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
                    var asignaturas = JsonConvert.DeserializeObject<ObservableCollection<AsignaturaModel>>(response.Data.ToString());

                    if (asignaturas != null && asignaturas.Count > 0)
                    {
                        ListaAsignaturas = asignaturas;
                        IsCursosVisible = false;
                        IsAsignaturasVisible = true;
                        IsLibrosVisible = false;
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Información", "No hay asignaturas registradas para este curso.", "Aceptar");
                    }
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.StackTrace);
                    Debug.Write(ex.ToString());
                    Debug.Write(ex.Message);
                }
            }
        }


        [RelayCommand]
        public async Task ObtenerLibros()
        {
            RequestModel request = new RequestModel()
            {
                Method = "GET",
                Data = string.Empty,
                Route = "http://localhost:8080/asignaturas/librosAsignaturas/" + SelectedAsignatura.IdAsignatura
            };

            ResponseModel response = await APIService.ExecuteRequest(request);
            if (response.Success.Equals(0))
            {
                try
                {
                    var libros = JsonConvert.DeserializeObject<ObservableCollection<LibroModel>>(response.Data.ToString());

                    if (libros != null && libros.Count > 0)
                    {
                        ListaLibros = libros;
                        IsCursosVisible = false;
                        IsAsignaturasVisible = false;
                        IsLibrosVisible = true;
                    }
                   
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.StackTrace);
                    Debug.Write(ex.ToString());
                    Debug.Write(ex.Message);
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Información", "No hay libros registrados para esta asignatura.", "Aceptar");
            }
        }


        [RelayCommand]
        public void VolverAtrasDesdeAsignaturas()
        {
            IsAsignaturasVisible = false;
            IsCursosVisible = true;
        }

        [RelayCommand]
        public void VolverAtrasDesdeLibros()
        {
            IsLibrosVisible = false;
            IsAsignaturasVisible = true;
        }

    }
}
