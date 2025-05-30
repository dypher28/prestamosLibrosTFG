using prestamosLibrosTFG.Models;
using prestamosLibrosTFG.ViewModels;
using System.Collections.ObjectModel;

namespace prestamosLibrosTFG.Views;

public partial class PaquetesView : ContentPage
{

    private bool isChanging = false;

    public PaquetesView()
	{
		InitializeComponent();
        BindingContext = new PaqueteViewModel();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var viewModel = BindingContext as PaqueteViewModel;
        viewModel?.InitViewCommand.Execute(null);

    }

    private void CollectionViewLibros_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (BindingContext is PaqueteViewModel vm)
        {
            if (vm.SelectedCurso == null)
            {
                if (isChanging)
                    return;
                isChanging = true;
                collectionViewLibros.SelectedItems = new List<object>();
                Shell.Current.DisplayAlert("Aviso", "Primero debes seleccionar un curso para poder elegir libros.", "OK");
            }
            else
            {
                vm.LibrosSeleccionados = new ObservableCollection<object>(((CollectionView)sender).SelectedItems);
            }
        }
        isChanging = false;
    }
}