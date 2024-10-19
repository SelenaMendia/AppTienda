using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppTiendaComida.Models;
using AppTiendaComida.Services;
using AppTiendaComida.Utils;
using AppTiendaComida.Views;
using AppTiendaComida.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AppTiendaComida.ViewModels
{
    public partial class ProductoListaViewModel : BaseViewModel
    {

        //[ObservableProperty] private ObservableCollection<Producto> _productos;
        //[ObservableProperty] private Producto _productoSeleccionado;
        //[ObservableProperty] private bool isRefreshing;

        //public ProductoListaViewModel()
        //{
        //    Title = Constants.AppName;

        //    Task.Run(async () => { await GetProductos(); }).Wait();
        //}

        //[RelayCommand]
        //private async Task GetProductos()
        //{
        //    IsBusy = true;

        //    var productos = await ApiService.GetProductos();

        //    if (productos != null)
        //    {
        //        Productos = new ObservableCollection<Producto>(productos);
        //    }

        //    IsBusy = false; 
        //}

        ////[RelayCommand]
        ////private async Task GoToDetalle()
        ////{
        ////    await Application.Current.MainPage.Navigation.PushAsync(new ProductoModificarPage());
        ////}

        ////[RelayCommand]
        ////private async Task NuevoProducto()
        ////{
        ////    await Application.Current.MainPage.Navigation.PushAsync(new ProductoAgregarPage());
        ////}
        ///

        //este funciona 

        //[ObservableProperty] private ObservableCollection<Producto> _productos = new ObservableCollection<Producto>();
        //[ObservableProperty] private Producto _productoSeleccionado;
        //[ObservableProperty] private bool isRefreshing;

        ////[ObservableProperty] private string searchText = "";
        //private ObservableCollection<Producto> originalProductos = new ObservableCollection<Producto>();

        //public ProductoListaViewModel()
        //{
        //    Title = Constants.AppName;
        //    GetProductos(); // Evitar el uso de Task.Run
        //}

        //[RelayCommand]
        //private async Task GetProductos()
        //{
        //    IsBusy = true;

        //    var productos = await ApiService.GetProductos();
        //    if (productos != null)
        //    {
        //        Productos = new ObservableCollection<Producto>(productos);
        //    }

        //    IsBusy = false;
        //}



        //[RelayCommand]
        //private async Task GoToDetalle(int productoId)
        //{
        //    //await Application.Current.MainPage.Navigation.PushAsync(new ProductoModificarPage());
        //    Producto producto = await ApiService.GetProductoPorId(productoId);
        //}

        //[RelayCommand]
        //private async Task ModificarProducto()
        //{
        //    await Application.Current.MainPage.Navigation.PushAsync(new ProductoModificarPage());
        //}

        //[RelayCommand]
        //private async Task NuevoProducto()
        //{
        //    await Application.Current.MainPage.Navigation.PushAsync(new ProductoAgregarPage());
        //}
        //hasta aca

        //public string SearchText
        //{
        //    get { return searchText; }
        //    set
        //    {
        //        if (SetProperty(ref searchText, value))
        //        {
        //            PerformSearchCommand.Execute(null);
        //        }
        //    }
        //}

        //[RelayCommand]
        //private async Task GoToDetalle(int productoId)
        //{
        //    // Obtener el producto usando el productoId
        //    Producto producto = await ApiService.GetProductoPorId(productoId);

        //    if (producto != null)
        //    {
        //        // Navegar a la página de detalles con el producto obtenido
        //        await Application.Current.MainPage.Navigation.PushAsync(new ProductoDetallePage
        //        {
        //            BindingContext = new ProductoDetalleViewModel
        //            {
        //                ProductoModel = producto
        //            }
        //        });
        //    }
        //}

        //[RelayCommand]
        //private async Task GoToDetalle()
        //{
        //    await Application.Current.MainPage.Navigation.PushAsync(new ProductoModificarPage());
        //}
        // Comando para manejar la selección del producto
        //[RelayCommand]
        //private async Task ProductoSeleccionadoChanged()
        //{
        //    if (ProductoSeleccionado != null)
        //    {
        //        // Llama al servicio para obtener los detalles del producto
        //        var productoDetalles = await ApiService.GetProductoPorId(ProductoSeleccionado.ProductoId);

        //        // Navega a la página de detalles del producto con el modelo obtenido
        //        if (productoDetalles != null)
        //        {
        //            var productoDetallePage = new ProductoDetallePage();
        //            var viewModel = new ProductoDetalleViewModel(productoDetalles);
        //            productoDetallePage.BindingContext = viewModel;
        //            await Shell.Current.GoToAsync(nameof(ProductoDetallePage), true);
        //        }

        //        // Resetea la selección
        //        ProductoSeleccionado = null;
        //    }
        //}


        //// Método para cargar los productos (simulado o desde una API)
        //private async Task CargarProductos()
        //{
        //    IsBusy = true;
        //    try
        //    {
        //        // Simulación de datos o puedes cargar los productos desde tu API
        //        var productos = await ApiService.GetProductos();
        //        foreach (var producto in productos)
        //        {
        //            Productos.Add(producto);
        //        }
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }
        //}

        //[RelayCommand]
        //private async Task GoToDetalle()
        //{
        //    await Application.Current.MainPage.Navigation.PushAsync(new ProductoModificarPage());
        //}

        //[RelayCommand]
        //private async Task NuevoProducto()
        //{
        //    await Application.Current.MainPage.Navigation.PushAsync(new ProductoAgregarPage());
        //}

        //[RelayCommand]
        //private async Task GoToDetalle()
        //{
        //    if (ProductoSeleccionado != null)
        //    {
        //        var detallePage = new ProductoDetallePage();
        //        detallePage.BindingContext = new ProductoDetalleViewModel(ProductoSeleccionado);
        //        await Application.Current.MainPage.Navigation.PushAsync(detallePage);
        //    }
        //}

        //[RelayCommand]
        //private void PerformSearch()
        //{
        //    if (string.IsNullOrWhiteSpace(SearchText))
        //    {
        //        Productos = new ObservableCollection<Producto>(originalProductos);
        //    }
        //    else
        //    {
        //        var filteredProducts = originalProductos
        //            .Where(p => p.Nombre.ToLower().Contains(SearchText.ToLower()))
        //            .ToList();

        //        Productos.Clear();
        //        foreach (var product in filteredProducts)
        //        {
        //            Productos.Add(product);
        //        }
        //    }
        //}
        [ObservableProperty] private ObservableCollection<Producto> _productos;
        [ObservableProperty] private Producto _productoSeleccionado;
        [ObservableProperty] private bool isRefreshing;


        public ProductoListaViewModel()
        {
            Title = Constants.AppName;

            Task.Run(async () => { await GetProductos(); }).Wait();
        }

        [RelayCommand]
        private async Task GetProductos()
        {
            IsBusy = IsRefreshing = true;

            var productos = await ApiService.GetProductos();

            if (productos != null)
            {
                Productos = new ObservableCollection<Producto>(productos);
            }

            IsBusy = IsRefreshing = false;
        }

        //[RelayCommand]
        //private async Task GoToDetalle()
        //{
        //    //await Application.Current.MainPage.Navigation.PushAsync(new ProductoModificarPage());
        //    await Application.Current.MainPage.Navigation.PushAsync(new ProductoDetallePage());

        //}

        //[RelayCommand]
        //private async Task GoToDetalle()
        //{
        //    if (ProductoSeleccionado != null) // Asegúrate de que haya un producto seleccionado
        //    {
        //        int productoId = ProductoSeleccionado.ProductoId; // Suponiendo que tu clase Producto tiene una propiedad Id
        //        await Application.Current.MainPage.Navigation.PushAsync(new ProductoDetallePage(productoId));
        //    }
        //}

        // Este método se ejecuta cuando se selecciona un producto
        partial void OnProductoSeleccionadoChanged(Producto value)
        {
            if (value != null)
            {
                // Llama al comando para obtener los detalles del producto
                GoToDetalleCommand.Execute(null);
            }
        }

        // Comando para obtener los detalles del producto y navegar a la página de detalles
        [RelayCommand]
        private async Task GoToDetalle()
        {
            if (ProductoSeleccionado != null)
            {
                try
                {
                    // Llamar al método GetProductoPorId para obtener los detalles del producto
                    var productoDetalles = await ApiService.GetProductoPorId(ProductoSeleccionado.ProductoId);

                    if (productoDetalles != null)
                    {
                        // Navega a la página de detalles con la información del producto obtenido
                        await Application.Current.MainPage.Navigation.PushAsync(new ProductoDetallePage(ProductoSeleccionado));
                    }
                }
                catch (Exception ex)
                {
                    // Manejar errores si es necesario, por ejemplo, mostrar un mensaje de error al usuario
                    await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }

        [RelayCommand]
        private async Task NuevoProducto()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ProductoAgregarPage());
        }

        [RelayCommand]
        private async Task MenuProducto()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new InicioPage());
        }

    }
}
