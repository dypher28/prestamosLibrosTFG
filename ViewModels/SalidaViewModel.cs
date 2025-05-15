using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace prestamosLibrosTFG.ViewModels
{
    public partial class SalidaViewModel : ObservableObject
    {
        public ICommand SalirCommand { get; }
        public ICommand CancelarCommand { get; }

        public SalidaViewModel()
        {
            SalirCommand = new RelayCommand(OnSalir);
            CancelarCommand = new RelayCommand(OnCancelar);
        }

        private void OnSalir()
        {
            // Cerrar la aplicación
            Environment.Exit(0);
        }

        private async void OnCancelar()
        {
            // Navegar a la vista de inicio
            await Shell.Current.GoToAsync("//MainView");
        }
    }
}
