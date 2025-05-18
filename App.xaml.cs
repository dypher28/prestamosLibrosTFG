using prestamosLibrosTFG.Views;

namespace prestamosLibrosTFG
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new SplashPage();
            MostrarInicio();
        }

        private async void MostrarInicio()
        {
            await Task.Delay(4000);
            MainPage = new NavigationPage(new AppShell()); 
        }

    }
}
