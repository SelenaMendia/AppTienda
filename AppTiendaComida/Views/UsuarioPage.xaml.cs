using AppTiendaComida.ViewModels;

namespace AppTiendaComida.Views;

public partial class UsuarioPage : ContentPage
{
	public UsuarioPage(UsuariosViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }


}