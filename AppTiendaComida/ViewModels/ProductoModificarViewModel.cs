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
using AppTiendaComida.Views;

namespace AppTiendaComida.ViewModels
{
    public partial class ProductoModificarViewModel : BaseViewModel
    {
        // Propiedades observables para los campos de producto
        [ObservableProperty] private string nombre;
        [ObservableProperty] private string descripcion;
        [ObservableProperty] private int stock;
        [ObservableProperty] private decimal precio;
        [ObservableProperty] private string rutaImagen;  // Ruta de la imagen seleccionada
        [ObservableProperty] private FileResult imagen;  // Archivo de la imagen seleccionado

        // Propiedad observable que contiene el producto actual
        [ObservableProperty]
        private Producto producto;

        // Constructor que inicializa el ViewModel con los datos del producto existente
        public ProductoModificarViewModel(int productoId)
        {
            CargarDatosProducto(productoId);
        }

        // Método para cargar los datos del producto existente desde la API
        private async void CargarDatosProducto(int productoId)
        {
            try
            {
                // Obtener los datos actuales del producto desde el servicio
                Producto = await ApiService.GetProductoPorId(productoId);

                // Rellenar las propiedades con los valores del producto actual
                Nombre = Producto.Nombre;
                Descripcion = Producto.Descripción;
                Stock = Producto.Stock;
                Precio = Producto.Precio;
                RutaImagen = Producto.ImagenUrl;  // Ruta de la imagen actual
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al cargar el producto: {ex.Message}", "Aceptar");
            }
        }

        // Comando para seleccionar una imagen desde la galería
        [RelayCommand]
        private async Task FotoGaleria()
        {
            try
            {
                // Seleccionar una imagen de la galería
                if (MediaPicker.Default.IsCaptureSupported)
                {
                    FileResult foto = await MediaPicker.PickPhotoAsync();
                    if (foto != null)
                    {
                        // Guardar la imagen seleccionada en el dispositivo
                        string localFilePath = Path.Combine(FileSystem.CacheDirectory, foto.FileName);
                        using Stream source = await foto.OpenReadAsync();
                        using FileStream fileStream = File.OpenWrite(localFilePath);
                        await source.CopyToAsync(fileStream);

                        // Actualizar la ruta y la imagen
                        RutaImagen = localFilePath;
                        Imagen = foto;
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Error al seleccionar la imagen", "Aceptar");
            }
        }

        // Comando para tomar una foto con la cámara
        [RelayCommand]
        private async Task TomarFoto()
        {
            try
            {
                if (MediaPicker.Default.IsCaptureSupported)
                {
                    FileResult foto = await MediaPicker.CapturePhotoAsync();
                    if (foto != null)
                    {
                        string localFilePath = Path.Combine(FileSystem.CacheDirectory, foto.FileName);
                        using Stream source = await foto.OpenReadAsync();
                        using FileStream fileStream = File.OpenWrite(localFilePath);
                        await source.CopyToAsync(fileStream);

                        RutaImagen = localFilePath;
                        Imagen = foto;
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Error al tomar foto", "Aceptar");
            }
        }

        // Comando para modificar el producto
        [RelayCommand]
        private async Task ModificarProducto()
        {
            // Validar los datos de entrada
            if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Descripcion) || Stock <= 0 || Precio <= 0)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Datos incompletos. Verifique!", "Aceptar");
                return;
            }

            // Actualizar solo los campos modificados
            Producto.Nombre = !string.IsNullOrEmpty(Nombre) ? Nombre : Producto.Nombre;
            Producto.Descripción = !string.IsNullOrEmpty(Descripcion) ? Descripcion : Producto.Descripción;
            Producto.Stock = Stock > 0 ? Stock : Producto.Stock;
            Producto.Precio = Precio > 0 ? Precio : Producto.Precio;

            // Si hay una nueva imagen seleccionada, actualizarla
            if (Imagen != null)
            {
                Producto.ImagenUrl = RutaImagen;
                Producto.Imagen = Imagen;
            }

            try
            {
                // Llamar al servicio API para modificar el producto
                await ApiService.ModificarProductoConImagen(Producto);

                // Notificar al usuario del éxito
                await Application.Current.MainPage.DisplayAlert("Éxito", "Producto modificado exitosamente.", "Aceptar");

                // Navegar de vuelta a la lista de productos
                await Application.Current.MainPage.Navigation.PushAsync(new ProductoListaPage(new ProductoListaViewModel()));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al modificar el producto: {ex.Message}", "Aceptar");
            }
        }

        // Comando para cancelar la modificación y volver atrás
        [RelayCommand]
        private async Task Cancelar()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
