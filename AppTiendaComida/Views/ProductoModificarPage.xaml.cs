using AppTiendaComida.Models;
using AppTiendaComida.ViewModels;

namespace AppTiendaComida.Views;

public partial class ProductoModificarPage : ContentPage
{
    //public ProductoModificarPage()
    //{
    //	InitializeComponent();
    //}

    //public ProductoModificarPage(Producto ProductoSeleccionado)
    //{
    //    InitializeComponent();

    //    ProductoModificarViewModel vm = new ProductoModificarViewModel(ProductoSeleccionado);

    //    BindingContext = vm;

    //    vm.ProductoSeleccionado = ProductoSeleccionado;

    //}

    public ProductoModificarPage(Producto ProductoSeleccionado)
    {
        InitializeComponent();
        ProductoModificarViewModel vm = new ProductoModificarViewModel(ProductoSeleccionado);
        BindingContext = vm;
        vm.Producto = ProductoSeleccionado;
    }
}