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
    public partial class InfoPrestamosViewModel : ObservableObject
    {

        [ObservableProperty]
        private ObservableCollection<PrestamoModel> listaPrestamos;

        [ObservableProperty]
        private PrestamoModel selectedPrestamo;

        [ObservableProperty]
        private PrestamoModel prestamo;

        [RelayCommand]
        public async Task InitView()
        {
            Prestamo = new PrestamoModel();
            OnPropertyChanged(nameof(prestamo));
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
                    ListaPrestamos =
                        JsonConvert.DeserializeObject<ObservableCollection<PrestamoModel>>(response.Data.ToString());
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.StackTrace);
                    Debug.Write(ex.ToString);
                    Debug.Write(ex.Message);

                }
            }
        }
    }
}
