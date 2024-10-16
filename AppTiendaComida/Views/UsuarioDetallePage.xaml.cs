using AppTiendaComida.Models;

namespace AppTiendaComida.Views;

public partial class UsuarioDetallePage : ContentPage
{
	
    private Usuario _usuarioDetalles;

    public UsuarioDetallePage(Usuario usuarioDetalles)
    {
       InitializeComponent();

        _usuarioDetalles = usuarioDetalles;
        BindingContext = _usuarioDetalles;

    }
}