using prestamosLibrosTFG.ViewModels;

namespace prestamosLibrosTFG.Views;

public partial class InfoPrestamosView : ContentPage
{
	public InfoPrestamosView()
	{
		InitializeComponent();
        BindingContext = new InfoPrestamosViewModel();
    }
}