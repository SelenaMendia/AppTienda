using AppTiendaComida.ViewModels;

namespace AppTiendaComida.Views;

public partial class ProductoAgregarPage : ContentPage
{
	public ProductoAgregarPage()
	{
		InitializeComponent();
        BindingContext = new ProductoAgregarViewModel();
    }
}