using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AppTiendaComida.Models;
using AppTiendaComida.Services;
using AppTiendaComida.Utils;
using AppTiendaComida.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppTiendaComida.ViewModels
{
    //public partial class ProductoDetalleViewModel : ObservableObject
    //{
    //    private Producto productoModel;

    //    public Producto ProductoModel
    //    {
    //        get { return productoModel; }
    //        set { SetProperty(ref productoModel, value); }
    //    }
    //    private async Task GoToBack()
    //    {
    //        await Application.Current.MainPage.Navigation.PopAsync();


    //    }
    //}

    //public partial class ProductoDetalleViewModel : BaseViewModel
    //{
    //    [ObservableProperty]
    //    private Producto _producto;

    //    public ProductoDetalleViewModel(Producto producto)
    //    {
    //        _producto = producto;
    //    }

    //    // Comando para regresar a la página anterior
    //    [RelayCommand]
    //    private async Task Volver()
    //    {
    //        await Shell.Current.GoToAsync(".."); // Navegar de vuelta a la página anterior
    //    }
    //}

    //public partial class ProductoDetalleViewModel : ObservableObject
    //{
    //    private Producto producto;//Variable para almacenar el producto seleccionado que se esta viendo.

    //    public Producto Producto//Propiedad para acceder al detalle del producto.
    //    {
    //        get { return producto; }//Devolvemos el producto que nosotros queremos ver.
    //        set { SetProperty(ref producto, value); }//Actualizamos el producto.
    //    }

    //}


    [QueryProperty(nameof(ProductoId), "ProductoId")]
    public partial class ProductoDetalleViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Producto> _productos;

        [ObservableProperty]
        private Producto _productoSeleccionado;

        [ObservableProperty] // Asegúrate de que esta propiedad esté observable
        private bool isBusy; // Indica si el ViewModel está ocupado

        [ObservableProperty]
        private string title; // Propiedad para el título


        [ObservableProperty]
        private bool isRefreshing;

        private int productoId; // Almacena el ID del producto

        public int ProductoId
        {
            get => productoId;
            set
            {
                productoId = value;
                _ = GetProductoPorId(productoId); // Llama a obtener el producto por ID de manera asíncrona
            }
        }
        public ProductoDetalleViewModel()
        {
            title = Constants.AppName;

            //cantidad
            Cantidad = 1;

            // Inicializamos los comandos
            IncrementarCantidadCommand = new RelayCommand(IncrementarCantidad);
            DisminuirCantidadCommand = new RelayCommand(DisminuirCantidad);

            
        }


        private async Task GetProductoPorId(int id)
        {
            isBusy = true; // Indica que estamos ocupados
            try
            {
                var producto = await ApiService.GetProductoPorId(id);
                if (producto != null)
                {
                    _productoSeleccionado = producto; // Asigna el producto obtenido
                }
                else
                {
                    Console.WriteLine("Producto no encontrado.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar el producto: {ex.Message}");
            }
            finally
            {
                isBusy = false; // Finaliza la operación
            }
        }

        [RelayCommand]
        private async Task Volver()
        {
            


            await Application.Current.MainPage.Navigation.PopAsync();


        }

        [RelayCommand]
        private async Task ModificarProducto()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ProductoModificarPage());

            //var navigationPage = Application.Current.MainPage as NavigationPage;
            //if (navigationPage != null)
            //{
            //    await navigationPage.PushAsync(new ProductoModificarPage());
            //}
            //else
            //{
            //    await Application.Current.MainPage.DisplayAlert("Error", "Navegación no disponible", "OK");
            //}
        }


        //ESTO ES NUEVO 
            private Producto _productoDetalles;

            public Producto ProductoDetalles
            {
                get => _productoDetalles;
                set
                {
                    _productoDetalles = value;
                    OnPropertyChanged(nameof(ProductoDetalles));
                }
            }

            // Constructor
            public ProductoDetalleViewModel(Producto productoDetalles)
            {
                ProductoDetalles = productoDetalles;
            }

            // Implementación de INotifyPropertyChanged
            public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }


        private int _cantidad;

        public int Cantidad
        {
            get => _cantidad;
            set
            {
                SetProperty(ref _cantidad, value);
            }
        }

        // Comandos para incrementar y disminuir la cantidad
        public ICommand IncrementarCantidadCommand { get; }
        public ICommand DisminuirCantidadCommand { get; }


        public void IncrementarCantidad()
        {
            Cantidad++;
        }

        // Método para disminuir la cantidad
        public void DisminuirCantidad()
        {
            if (Cantidad > 0)
            {
                Cantidad--;
            }
        }



    }
}
