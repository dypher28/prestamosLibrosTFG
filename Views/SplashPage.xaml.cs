namespace prestamosLibrosTFG.Views;

public partial class SplashPage : ContentPage
{
    private bool animando = true;

    public SplashPage()
    {
        InitializeComponent();
        IniciarSplash();
        AnimarTextoCargando();
    }

    private async void IniciarSplash()
    {
        await Task.Delay(3000);
        await Navigation.PopModalAsync();
    }

    private async void AnimarTextoCargando()
    {
        var baseTexto = "Cargando";
        var puntos = new[] { ".", "..", "..." };

        while (animando)
        {
            foreach (var p in puntos)
            {
                CargandoLabel.Text = baseTexto + p;
                await Task.Delay(300);
            }
        }
    }

    protected override void OnDisappearing()
    {
        animando = false; 
        base.OnDisappearing();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Animaci�n para el logo
        await LogoImage.FadeTo(1, 600); 
        await LogoImage.TranslateTo(0, 0, 600, Easing.CubicOut); 
        await LogoImage.ScaleTo(1.1, 200);
        await LogoImage.ScaleTo(1.0, 200);

        // Esperar 2 segundos y navegar a la p�gina principal
        await Task.Delay(2000);
        animando = false;
    }
}