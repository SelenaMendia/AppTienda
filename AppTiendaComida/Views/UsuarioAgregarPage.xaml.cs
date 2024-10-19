using AppTiendaComida.Services;
using AppTiendaComida.ViewModels;

namespace AppTiendaComida.Views;

public partial class UsuarioAgregarPage : ContentPage
{
	//public UsuarioAgregarPage()
	//{
	//	InitializeComponent();
 //       BindingContext = new UsuarioAgregarViewModel(new ApiService());
 //   }

    public UsuarioAgregarPage(UsuarioAgregarViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}