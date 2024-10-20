using AppTiendaComida.Models;
using AppTiendaComida.ViewModels;

namespace AppTiendaComida.Views;

public partial class ProductoDetallePage : ContentPage
{
    //public ProductoDetallePage()
    //   {
    //	InitializeComponent();
    //       BindingContext = new ProductoDetalleViewModel();
    //   }
    //private Producto _productoDetalles;

    //public ProductoDetallePage(Producto productoDetalles)
    //{
    //    InitializeComponent();
    //    _productoDetalles = productoDetalles;
    //    BindingContext = _productoDetalles;

    //}

    public ProductoDetallePage(Producto ProductoSeleccionado)
    {
        InitializeComponent();

        ProductoDetalleViewModel vm = new ProductoDetalleViewModel(ProductoSeleccionado);

        BindingContext = vm;

        vm.ProductoSeleccionado = ProductoSeleccionado;

    }



}