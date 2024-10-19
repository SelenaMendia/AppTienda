using AppTiendaComida.Models;
using AppTiendaComida.ViewModels;

namespace AppTiendaComida.Views;

public partial class UsuarioModificarPage : ContentPage
{

    public UsuarioModificarPage(Usuario UsuarioSeleccionado)
    {
        InitializeComponent();
        UsuarioModificarViewModel vm = new UsuarioModificarViewModel(UsuarioSeleccionado);
        BindingContext = vm;
        vm.Usuario = UsuarioSeleccionado;
    }


}