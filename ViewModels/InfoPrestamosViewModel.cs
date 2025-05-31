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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prestamosLibrosTFG.ViewModels
{
    public partial class InfoPrestamosViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<MostrarPrestamoModel> listaPrestamos;

        [ObservableProperty]
        private ObservableCollection<MatriculaModel> listaMatriculas;

        [ObservableProperty]
        private ObservableCollection<MostrarPrestamoModel> _todosLosPrestamos;

        private ObservableCollection<MatriculaModel> _todasLasMatriculas;

        [ObservableProperty]
        private PrestamoModel selectedPrestamo;

        [ObservableProperty]
        private PrestamoModel prestamo;

        [ObservableProperty]
        private MostrarPrestamoModel mostrarPrestamo;

        [ObservableProperty]
        private string filtroNombrePrestamo;

        [ObservableProperty]
        private string filtroNombreAlumno;


        [RelayCommand]
        public async Task InitView()
        {
            Prestamo = new PrestamoModel();
            OnPropertyChanged(nameof(Prestamo));
            ObtenerPrestamos();
        }

        [RelayCommand] // Metodo para obtener los préstamos  
        public async Task ObtenerPrestamos()
        {
            RequestModel request = new RequestModel()
            {
                Method = "GET",
                Data = string.Empty,
                Route = "http://localhost:8080/prestamos/obtenerPrestamos"
            };

            ResponseModel response = await APIService.ExecuteRequest(request);
            if (response.Success.Equals(0))
            {
                try
                {
                    var lista = JsonConvert.DeserializeObject<List<MostrarPrestamoModel>>(response.Data.ToString());

                    ListaPrestamos = new ObservableCollection<MostrarPrestamoModel>(lista);
                    _todosLosPrestamos = new ObservableCollection<MostrarPrestamoModel>(lista);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        [RelayCommand]
        public async Task BorrarPrestamo(MostrarPrestamoModel prestamo)
        {
            bool confirm = await App.Current.MainPage.DisplayAlert("Confirmación", $"¿Quieres cancelar este préstamo", "Sí", "No");
            if (confirm)
            {

                RequestModel request = new RequestModel()
                {
                    Method = "DELETE",
                    Data = string.Empty,
                    Route = "http://localhost:8080/prestamos/borrarPrestamo/" + prestamo.Id
                };

                ResponseModel response = await APIService.ExecuteRequest(request);
                if (response.Success.Equals(0))
                {
                    try
                    {
                        ListaPrestamos.Remove(prestamo);
                        await App.Current.MainPage.DisplayAlert("Información", "Préstamo cancelado correctamente", "Aceptar");

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
                    await App.Current.MainPage.DisplayAlert("Error", "Error al cancelar el préstamo", "Aceptar");
                }

            }
        }
        [RelayCommand]
        public async Task DevolverPrestamo(MostrarPrestamoModel prestamo)
        {
            bool confirm = await App.Current.MainPage.DisplayAlert("Confirmación", $"Se ha devuelto este préstamo", "Sí", "No");
            if (confirm)
            {

                RequestModel request = new RequestModel()
                {
                    Method = "PUT",
                    Data = string.Empty,
                    Route = "http://localhost:8080/prestamos/devolver/" + prestamo.Id
                };

                ResponseModel response = await APIService.ExecuteRequest(request);
                if (response.Success.Equals(0))
                {
                    try
                    {
                        await App.Current.MainPage.DisplayAlert("Información", "Préstamo devuelto", "Aceptar");

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
                    await App.Current.MainPage.DisplayAlert("Error", "Error al devolver el préstamo", "Aceptar");
                }

            }
        }

        [RelayCommand]
        public async Task FiltrarPrestamosPorNombre()
        {
            if (string.IsNullOrWhiteSpace(FiltroNombrePrestamo))
            {
                await ObtenerPrestamos(); // si el filtro está vacío, enseña todo
                return;
            }

            RequestModel request = new RequestModel()
            {
                Method = "GET",
                Data = string.Empty,
                Route = $"http://localhost:8080/paquetes/filtrarPorNombre?filtro={FiltroNombrePrestamo}"
            };

            ResponseModel response = await APIService.ExecuteRequest(request);
            if (response.Success.Equals(0))
            {
                try
                {
                    if (response.Data != null)
                    {
                        ListaPrestamos = JsonConvert.DeserializeObject<ObservableCollection<MostrarPrestamoModel>>(response.Data.ToString());
                    }
                    else
                    {
                        Debug.WriteLine("response.Data is null.");
                    }
                    ListaPrestamos = JsonConvert.DeserializeObject<ObservableCollection<MostrarPrestamoModel>>(response.Data.ToString());
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.ToString());
                }
            }
        }


        private void AplicarFiltros()
        {
            if (_todosLosPrestamos == null)
                return;

            var filtradas = _todosLosPrestamos.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(FiltroNombrePrestamo))
            {
                filtradas = filtradas.Where(m =>
                    m.Paquete.Nombre.ToLower().Contains(FiltroNombrePrestamo, StringComparison.OrdinalIgnoreCase));

                if (!filtradas.Any())
                {
                    filtradas = _todosLosPrestamos.AsEnumerable();

                    filtradas = filtradas.Where(m =>
                        RemoveDiacritics(m.Matricula.Alumno.Nombre).ToLower().Contains(RemoveDiacritics(FiltroNombrePrestamo).ToLower(), StringComparison.InvariantCultureIgnoreCase) ||
                         RemoveDiacritics(m.Matricula.Alumno.Apellidos).ToLower().Contains(RemoveDiacritics(FiltroNombrePrestamo).ToLower(), StringComparison.InvariantCultureIgnoreCase));
                }
            }

            ListaPrestamos = new ObservableCollection<MostrarPrestamoModel>(filtradas);
        }

        partial void OnFiltroNombrePrestamoChanged(string value)
        {
            AplicarFiltros();
        }

        private string RemoveDiacritics(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            var normalized = text.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (var c in normalized)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }

    }
}
