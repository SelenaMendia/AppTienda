using AppTiendaComida.Models;
using AppTiendaComida.ViewModels;

namespace AppTiendaComida.Views;

public partial class UsuarioModificarPage : ContentPage
{

    public UsuarioModificarPage(Usuario UsuarioSeleccionado)
    {
        InitializeComponent();
        UsuarioModificarViewModel vm = new UsuarioModificarViewModel();
        BindingContext = vm;
        vm.Usuario = UsuarioSeleccionado;
    }
}