using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppTiendaComida.Models;
using AppTiendaComida.Services;
using AppTiendaComida.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Http;

namespace AppTiendaComida.ViewModels
{
    public partial class ProductoModificarViewModel : BaseViewModel
    {
        [ObservableProperty]
        Producto _ProductoRecibido;

        [ObservableProperty]
        int id;

        [ObservableProperty]
        CrearProductoDto _ModificarProducto = new CrearProductoDto();

        [ObservableProperty]
        ImageSource _ImageSource;

        ApiService _servicio;
        public ProductoModificarViewModel()
        {
            Title = "Midifcación del producto";
            _servicio = new ApiService();
        }

        [RelayCommand]
        public async Task ModificarProductoAsync()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;

                    ModificarProducto.Nombre = ProductoRecibido.Nombre;
                    ModificarProducto.Precio = ProductoRecibido.Precio;
                    ModificarProducto.Descripción = ProductoRecibido.Descripción;
                    ModificarProducto.Stock = ProductoRecibido.Stock;



                    var response = _servicio.PutProductosAsync(id, ModificarProducto);
                    if (response != null)
                    {
                        IsBusy = false;
                        await App.Current.MainPage.DisplayAlert("Respuesta", "Producto modificado correctamente", "Ok");

                    }
                    else
                    {
                        IsBusy = false;
                        await App.Current.MainPage.DisplayAlert("Error catastrofico!", "Hubo un error al intentar cargar el producto", "Ok");

                    }
                }
                catch (Exception)
                {

                    IsBusy = false;
                    await App.Current.MainPage.DisplayAlert("Error catastrofico!", "Hubo un error al intentar cargar el producto", "Ok");
                }
                finally
                {
                    IsBusy = false;
                }
            }

        }

        [RelayCommand]
        public async Task BuscarPorIdAsync()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;
                    _ProductoRecibido = await _servicio.SearchByIdAsync(id);
                    if (_ProductoRecibido != null)
                    {
                        IsBusy = false;
                        //recargar las propiedades para que se vean
                        OnPropertyChanged(nameof(ProductoRecibido));

                    }
                    else
                    {
                        IsBusy = false;
                        await App.Current.MainPage.DisplayAlert("Error!", "Producto no encontrado", "Ok");

                    }
                    IsBusy = false;
                }
                catch (Exception ex)
                {
                    IsBusy = false;

                    await App.Current.MainPage.DisplayAlert("Error!", $"{ex.Message}", "Ok");
                }
                finally { IsBusy = false; }
            }
        }
        [RelayCommand]
        public async Task BuscarImagenAsync()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;

                    // Seleccionar la imagen usando FilePicker
                    FileResult? result = await FilePicker.PickAsync(new PickOptions
                    {
                        PickerTitle = "Selecciona una imagen"
                    });

                    if (result != null)
                    {
                        // Abre el stream de la imagen seleccionada
                        var stream = await result.OpenReadAsync();

                        // Asignar el archivo seleccionado a la propiedad Imagen del modelo ModificarProducto
                        using var memoryStream = new MemoryStream();
                        await stream.CopyToAsync(memoryStream);

                        // Crear un FormFile a partir del stream para que sea compatible con IFormFile
                        ModificarProducto.Imagen = new FormFile(memoryStream, 0, memoryStream.Length, result.FileName, result.FileName)
                        {
                            Headers = new HeaderDictionary(),
                            ContentType = result.ContentType ?? "application/octet-stream"
                        };

                        // Actualiza la imagen en la interfaz (UI) usando ImageSource
                        _ImageSource = ImageSource.FromStream(() => new MemoryStream(memoryStream.ToArray()));

                        // Asegura que el BindingContext se actualiza correctamente
                        Application.Current.MainPage.BindingContext = this;
                        OnPropertyChanged(nameof(ImageSource)); // Notificar que la imagen ha cambiado
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Error!", "Imagen no cargada", "Ok");
                    }
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Error!", ex.Message, "Ok");
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }


        [RelayCommand]
        public async Task GoBackAsync()
        {
            await Application.Current.MainPage.Navigation.PopAsync();

        }
    }
}
