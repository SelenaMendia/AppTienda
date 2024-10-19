using AppTiendaComida.ViewModels;

namespace AppTiendaComida.Views;

public partial class AcercaDe : ContentPage
{
	public AcercaDe()
	{
		InitializeComponent();
		BindingContext = new InicioViewModel();

    }
}