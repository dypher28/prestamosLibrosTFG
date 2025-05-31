﻿using CommunityToolkit.Mvvm.ComponentModel;
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
        private ObservableCollection<CursoModel> listaCursos; // Lista del picker para crear el paquete

        [ObservableProperty]
        private PaqueteModel paquete;

        [ObservableProperty]
        private ObservableCollection<LibroModel> listaLibros;

        [ObservableProperty]
        private ObservableCollection<AsignaturaModel> listaAsignaturas;

        [ObservableProperty]
        private CursoModel selectedCurso; // SelectedLibro para paquetes

        [ObservableProperty]
        private AsignaturaModel selectedAsignatura;

        [ObservableProperty]
        private LibroModel libro = new();

        [ObservableProperty]
        private ObservableCollection<object> librosSeleccionados;

        [ObservableProperty]
        private ObservableCollection<LibroModel> listaLibrosFiltrada;

        private CursoModel _cursoAnterior;

        private bool _isFirst = true;


        public PaqueteViewModel()
        {


        }

        [RelayCommand]
        public async Task InitView()
        {
            Paquete = new PaqueteModel();
            OnPropertyChanged(nameof(Paquete));
            LibrosSeleccionados = new ObservableCollection<object>();
            ObtenerLibros();
            _isFirst = true;
            SelectedAsignatura = null;
            SelectedCurso = null;
            ObtenerCursos();
        }

        partial void OnSelectedCursoChanged(CursoModel value)
        {
            if (_isFirst)
            {
                _cursoAnterior = value;

                FiltrarLibros();
                _isFirst = false;
            }
            else
            {
                ConfirmarCambioCurso(value);

            }
        }

        private async void ConfirmarCambioCurso(CursoModel nuevoCurso)
        {
            if (LibrosSeleccionados?.Count == 0 || LibrosSeleccionados == null)
            {
                FiltrarLibros();
            }
            else
            {
                if (nuevoCurso == _cursoAnterior || nuevoCurso == null) return;
                bool confirmado = await App.Current.MainPage.DisplayAlert(
                    "Confirmación",
                    $"¿Estás seguro de que quieres cambiar el curso a {nuevoCurso.Curso}?",
                    "Sí", "No");

                if (!confirmado)
                {
                    // Volver al curso anterior
                    SelectedCurso = _cursoAnterior;
                    return;
                }

                LibrosSeleccionados = null;

                ObtenerAsignaturasCommand.Execute(null);
                FiltrarLibros();

            }
            _cursoAnterior = nuevoCurso;
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
            SelectedAsignatura = null;
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


        [RelayCommand] // Comando para llamar en el boton de limpiar
        public void Limpiar()
        {
            Libro = new LibroModel
            {
                Imagen = new LibroModel.ImageInfo
                {
                    FileName = "librodefecto.png"
                }
            };

            LibrosSeleccionados = null;
            SelectedCurso = null;
            SelectedAsignatura = null;
            _isFirst = true;
            ObtenerLibros();

        }

        private async Task MostrarMensaje(string mensaje)
        {
            await App.Current.MainPage.DisplayAlert("Aviso", mensaje, "OK");
        }


        [RelayCommand]
        public async Task CrearPaquete()
        {
            if (string.IsNullOrWhiteSpace(Paquete?.Nombre))
            {
                await MostrarMensaje("Debes ingresar un nombre para el paquete.");
                return;
            }

            if (SelectedCurso == null)
            {
                await MostrarMensaje("Debes seleccionar un curso.");
                return;
            }

            if (LibrosSeleccionados == null || LibrosSeleccionados.Count == 0)
            {
                await MostrarMensaje("Debes seleccionar al menos un libro.");
                return;
            }

            Paquete.Libros = new HashSet<LibroInfoModel>(
            LibrosSeleccionados.OfType<LibroModel>().Select(libro => new LibroInfoModel
            {
                Id = libro.Id ?? 0,
                Titulo = libro.Titulo,
                Editorial = libro.Editorial,
                Cantidad = libro.Cantidad ?? 0,
                Isbn = libro.Isbn
            })
            );


            Paquete.Curso = new Curso
            {
                Id = int.Parse(SelectedCurso.IdCurso),
                Nombre = SelectedCurso.Curso
            };

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
                SelectedCurso = null;
                LibrosSeleccionados.Clear();
            }
            else
            {
                await MostrarMensaje("Error al crear el paquete. Inténtalo de nuevo.");
            }
        }
    }
}
