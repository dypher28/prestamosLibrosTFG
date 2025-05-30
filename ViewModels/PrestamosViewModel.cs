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
    public partial class PrestamosViewModel : ObservableObject
    {

        [ObservableProperty]
        private ObservableCollection<MatriculaModel> listaMatriculas;

        [ObservableProperty]
        private MatriculaModel selectedMatricula;

        [ObservableProperty]
        private ObservableCollection<PaqueteModel> listaPaquetes;

        [ObservableProperty]
        private PaqueteModel selectedPaquete;

        private ObservableCollection<MatriculaModel> _todasLasMatriculas;

        private ObservableCollection<PaqueteModel> _todosLosPaquetes;

        [ObservableProperty]
        private ObservableCollection<CursoModel> listaCursos;

        [ObservableProperty]
        private CursoModel selectedCurso;

        [ObservableProperty]
        private ObservableCollection<object> alumnosSeleccionados;

        [ObservableProperty]
        private string filtroNombreAlumno;

        private CursoModel _cursoAnterior;

        [ObservableProperty]
        private PrestamoModel prestamo;

        private bool _isFirst = true;

        [RelayCommand]
        public async Task Inicializar()
        {
            Prestamo = new PrestamoModel();
            OnPropertyChanged(nameof(Prestamo));
            AlumnosSeleccionados = new ObservableCollection<object>();
            await ObtenerMartriculas();
            await ObtenerPaquetes();
            await ObtenerCursos();
        }

        [RelayCommand]
        public async Task FiltrarMatriculasPorNombre()
        {
            if (string.IsNullOrWhiteSpace(FiltroNombreAlumno))
            {
                await ObtenerMartriculas(); // si el filtro está vacío, trae todo
                return;
            }

            RequestModel request = new RequestModel()
            {
                Method = "GET",
                Data = string.Empty,
                Route = $"http://localhost:8080/matriculas/filtrarPorNombre?filtro={FiltroNombreAlumno}"
            };

            ResponseModel response = await APIService.ExecuteRequest(request);
            if (response.Success.Equals(0))
            {
                try
                {
                    ListaMatriculas = JsonConvert.DeserializeObject<ObservableCollection<MatriculaModel>>(response.Data.ToString());
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.ToString());
                }
            }
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

        [RelayCommand]
        public async Task ObtenerMartriculas()
        {
            RequestModel request = new()
            {
                Method = "GET",
                Data = string.Empty,
                Route = "http://localhost:8080/matriculas/obtenerMatriculas"
            };

            ResponseModel response = await APIService.ExecuteRequest(request);
            if (response.Success.Equals(0))
            {
                try
                {
                    var lista = JsonConvert.DeserializeObject<List<MatriculaModel>>(response.Data.ToString());

                    ListaMatriculas = new ObservableCollection<MatriculaModel>(lista);
                    _todasLasMatriculas = new ObservableCollection<MatriculaModel>(lista);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        [RelayCommand] // Metodo para obtener los paquetes
        public async Task ObtenerPaquetes()
        {
            RequestModel request = new RequestModel()
            {
                Method = "GET",
                Data = string.Empty,
                Route = "http://localhost:8080/paquetes/obtenerPaquetes"
            };

            ResponseModel response = await APIService.ExecuteRequest(request);
            if (response.Success.Equals(0))
            {
                try
                {
                    _todosLosPaquetes = JsonConvert.DeserializeObject<ObservableCollection<PaqueteModel>>(response.Data.ToString());
                    ListaPaquetes = new ObservableCollection<PaqueteModel>(_todosLosPaquetes);
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
        public async Task BorrarPaquete(PaqueteModel paquete)
        {
            bool confirm = await App.Current.MainPage.DisplayAlert("Confirmación", $"¿Quieres borrar el paquete: {paquete.Nombre}?", "Sí", "No");
            if (confirm)
            {

                RequestModel request = new RequestModel()
                {
                    Method = "DELETE",
                    Data = string.Empty,
                    Route = "http://localhost:8080/paquetes/borrarPaquete/" + paquete.Id
                };

                ResponseModel response = await APIService.ExecuteRequest(request);
                if (response.Success.Equals(0))
                {
                    try
                    {
                        ListaPaquetes.Remove(paquete);
                        await App.Current.MainPage.DisplayAlert("Información", "Paquete borrado correctamente", "Aceptar");

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
                    await App.Current.MainPage.DisplayAlert("Error", "Error al borrar el paquete", "Aceptar");
                }
            }
        }

        partial void OnSelectedCursoChanged(CursoModel value)
        {
            ConfirmarCambioCurso(value);

        }


        private async void ConfirmarCambioCurso(CursoModel nuevoCurso)
        {
            if (_cursoAnterior != null && nuevoCurso != null && _cursoAnterior != nuevoCurso && AlumnosSeleccionados != null && SelectedPaquete != null)
            {
                bool confirmado = await App.Current.MainPage.DisplayAlert(
                    "Confirmación",
                    $"¿Estás seguro de que quieres cambiar el curso a {nuevoCurso.Curso}?",
                    "Sí", "No");

                if (!confirmado)
                {
                    SelectedCurso = _cursoAnterior; // revertir el cambio
                    return;
                }
            }
            AplicarFiltros();
            AlumnosSeleccionados = null;
            SelectedPaquete = null;
            _cursoAnterior = nuevoCurso;


        }


        [RelayCommand] // Comando para llamar en el boton de limpiar
        public void Limpiar()
        {
            SelectedCurso = null;
            Inicializar();
        }

        private void AplicarFiltros()
        {
            if (_todasLasMatriculas == null)
                return;

            var filtradas = _todasLasMatriculas.AsEnumerable();

            if (SelectedCurso != null)
            {
                var nombreCurso = SelectedCurso.Curso?.Trim().ToLower();
                filtradas = filtradas.Where(m => m.Curso?.NombreCurso?.Trim().ToLower() == nombreCurso);
            }

            if (!string.IsNullOrWhiteSpace(FiltroNombreAlumno))
            {
                var texto = FiltroNombreAlumno.ToLower().Trim();
                filtradas = filtradas.Where(m =>
                    m.Alumno.Nombre.ToLower().Contains(texto) ||
                    m.Alumno.Apellidos.ToLower().Contains(texto));
            }

            if (_todosLosPaquetes != null)
            {
                if (SelectedCurso == null) return;
                var nombreCurso = SelectedCurso.Curso?.Trim().ToLower();
                ListaPaquetes = new ObservableCollection<PaqueteModel>(
                    _todosLosPaquetes.Where(p => p.Curso?.Nombre?.Trim().ToLower() == nombreCurso));
            }

            ListaMatriculas = new ObservableCollection<MatriculaModel>(filtradas);
        }

        partial void OnFiltroNombreAlumnoChanged(string value)
        {
            AplicarFiltros();
        }

        [RelayCommand]
        public async Task CrearPrestamos()
        {
            if (SelectedPaquete == null || AlumnosSeleccionados == null || AlumnosSeleccionados.Count == 0)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debes seleccionar un paquete y al menos un alumno.", "OK");
                return;
            }

            var prestamos = new List<HacerPrestamoModel>();

            foreach (var alumnoObj in AlumnosSeleccionados)
            {
                if (alumnoObj is MatriculaModel matricula)
                {
                    prestamos.Add(new HacerPrestamoModel
                    {
                        Matricula = new MatriculaInfo { Id = matricula.Id },
                        Paquete = new PaqueteInfo1 { Id = SelectedPaquete.Id ?? 0 },
                        Devuelto = false
                    });
                }
            }

            var request = new RequestModel
            {
                Method = "POST",
                Data = prestamos,
                Route = "http://localhost:8080/prestamos/crearPrestamos"
            };

            var response = await APIService.ExecuteRequest(request);

            if (response.Success.Equals(0))
            {
                await App.Current.MainPage.DisplayAlert("Éxito", "Préstamos creados correctamente", "OK");


            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Error al crear préstamos: {response.Message}", "OK");

            }
            Limpiar();
            Inicializar();
        }

        [RelayCommand]
        public async Task EditarPaquete(PaqueteModel paquete)
        {

            if (paquete == null)
            {
                await App.Current.MainPage.DisplayAlert("Error", "NULL", "OK");
                return;
            }

            await App.Current.MainPage.DisplayAlert("Editar", "EDITAR PAQUETE", "OK");
            await Shell.Current.GoToAsync("//PaquetesView", new Dictionary<string, object>()
            {
                ["Paquete"] = paquete
            });

        }

    }
}
