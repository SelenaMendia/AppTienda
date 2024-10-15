using AppTiendaComida.ViewModels;

namespace AppTiendaComida.Views;

public partial class InicioPage : ContentPage
{
    public InicioPage()
    {
        InitializeComponent();
        BindingContext = new InicioViewModel();
    }

    //public InicioPage(InicioViewModel _viewModel)
    //{
    //    BindingContext = _viewModel;

    //    InitializeComponent();
    //}
}