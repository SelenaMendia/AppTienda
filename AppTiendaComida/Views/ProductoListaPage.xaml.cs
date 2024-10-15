using AppTiendaComida.ViewModels;

namespace AppTiendaComida.Views;

public partial class ProductoListaPage : ContentPage
{
	public ProductoListaPage(ProductoListaViewModel _viewModel)
	{
        BindingContext = _viewModel;

        InitializeComponent();
	}
}