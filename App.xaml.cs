using prestamosLibrosTFG.Views;

namespace prestamosLibrosTFG
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
            MostrarSplash();
        }

        private async void MostrarSplash()
        {
            await Task.Delay(100);
            await Application.Current.MainPage.Navigation.PushModalAsync(new SplashPage());
        }

    }
}
