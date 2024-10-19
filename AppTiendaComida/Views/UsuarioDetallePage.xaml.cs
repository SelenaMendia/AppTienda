using AppTiendaComida.Models;
using AppTiendaComida.ViewModels;

namespace AppTiendaComida.Views;

public partial class UsuarioDetallePage : ContentPage
{

    //private Usuario _usuarioDetalles;

    //public UsuarioDetallePage(Usuario usuarioDetalles)
    //{
    //   InitializeComponent();

    //    _usuarioDetalles = usuarioDetalles;
    //    BindingContext = _usuarioDetalles;

    //}

    public UsuarioDetallePage(Usuario UsuarioSeleccionado)
    {
        InitializeComponent();

        UsuarioDetalleViewModel vm = new UsuarioDetalleViewModel(UsuarioSeleccionado);

        BindingContext = vm;

        vm.UsuarioSeleccionado = UsuarioSeleccionado;

    }


    //public UsuarioDetallePage(UsuarioDetalleViewModel viewModel)
    //{
    //    InitializeComponent();
    //    BindingContext = viewModel;  // Aquí se asigna el ViewModel correcto
    //}
}