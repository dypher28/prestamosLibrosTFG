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
    public partial class PrestamosViewModel: ObservableObject
    {

        [ObservableProperty]
        private ObservableCollection<MatriculaModel> listaMatriculas;

        [ObservableProperty]
        private MatriculaModel selectedMatricula;

        [ObservableProperty]
        private ObservableCollection<PaqueteModel> listaPaquetes;

        [ObservableProperty]
        private PaqueteModel selectedPaquete;

        [RelayCommand] // Metodo para obtener las matriculas
        public async Task ObtenerMartriculas()
        {
            RequestModel request = new RequestModel()
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
                    ListaMatriculas =
                        JsonConvert.DeserializeObject<ObservableCollection<MatriculaModel>>(response.Data.ToString());

                }
                catch (Exception ex)
                {
                    Debug.Write(ex.StackTrace);
                    Debug.Write(ex.ToString);
                    Debug.Write(ex.Message);

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
                    ListaPaquetes =
                        JsonConvert.DeserializeObject<ObservableCollection<PaqueteModel>>(response.Data.ToString());

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
