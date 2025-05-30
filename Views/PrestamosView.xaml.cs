using prestamosLibrosTFG.ViewModels;
using System.Collections.ObjectModel;

namespace prestamosLibrosTFG.Views;

public partial class PrestamosView : ContentPage
{

    private bool isChanging = false;
    public PrestamosView()
	{
		InitializeComponent();
        BindingContext = new PrestamosViewModel();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        if (BindingContext is PrestamosViewModel vm)
        {
            vm.Limpiar();
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var viewModel = BindingContext as PrestamosViewModel;
        viewModel?.InicializarCommand.Execute(null);

    }

    private void CollectionViewMatriculas_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (BindingContext is PrestamosViewModel vm)
        {
            if (vm.SelectedCurso == null)
            {
                if (isChanging)
                    return;
                isChanging = true;
                collectionViewMatriculas.SelectedItems = new List<object>();
                Shell.Current.DisplayAlert("Aviso", "Primero debes seleccionar un curso para poder elegir un alumno.", "OK");
            }
            else
            {
                vm.AlumnosSeleccionados = new ObservableCollection<object>(((CollectionView)sender).SelectedItems);
            }
        }
        isChanging = false;
    }
}