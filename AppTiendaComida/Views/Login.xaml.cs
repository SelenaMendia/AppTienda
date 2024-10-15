using AppTiendaComida.ViewModels;

namespace AppTiendaComida.Views;

public partial class Login : ContentPage
{
    private LoginViewModel viewModel;
    public Login()
    {
        BindingContext = viewModel = new LoginViewModel();
        InitializeComponent();
    }
}