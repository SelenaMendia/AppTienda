using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppTiendaComida.Models;
using AppTiendaComida.Services;
using AppTiendaComida.Utils;
using AppTiendaComida.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Maui;



namespace AppTiendaComida.ViewModels
{
    public partial class ProductoAgregarViewModel : BaseViewModel
    {
        //[ObservableProperty] private string nombre;
        //[ObservableProperty] private string descripción;
        //[ObservableProperty] private int stock;
        //[ObservableProperty] private float precio;
        //[ObservableProperty] private byte[] imagen; // Nueva propiedad para la imagen
        //[ObservableProperty] List<Valor> listaCategorias;
        //[ObservableProperty] private Valor categoriaSeleccionada;

        //public ProductoAgregarViewModel()
        //{
        //    Title = Constants.AppName;
        //    listaCategorias = GetCategoriasValues();
        //}

        //[RelayCommand]
        //private async Task Cancelar()
        //{
        //    await Application.Current.MainPage.Navigation.PopAsync();
        //}

        //[RelayCommand]
        //private async Task GrabarProducto()
        //{
        //    if (string.IsNullOrEmpty(this.Nombre))
        //    {
        //        await Application.Current.MainPage.DisplayAlert("Error", "El campo Nombre es obligatorio.", "Aceptar");
        //        return;
        //    }

        //    if (string.IsNullOrEmpty(this.Descripción))
        //    {
        //        await Application.Current.MainPage.DisplayAlert("Error", "El campo Descripción es obligatorio.", "Aceptar");
        //        return;
        //    }

        //    var registro = new Producto
        //    {
        //        CrearProducto.Nombre = this.Nombre,
        //        Descripción = this.Descripción,
        //        Stock = this.Stock,
        //        Precio = Convert.ToDecimal(this.Precio),
        //        CategoriaId = this.CategoriaSeleccionada.Key,
        //        ImagenUrl = "producto.png" // Reemplaza con la URL real de la imagen
        //    };

        //    try
        //    {
        //        // Agregar producto con imagen
        //        bool success = await ApiService.AgregarProducto(registro, imagen);
        //        if (success)
        //        {
        //            await Application.Current.MainPage.DisplayAlert("Éxito", "Nuevo producto agregado.", "Aceptar");
        //            await Application.Current.MainPage.Navigation.PopAsync();
        //        }
        //        else
        //        {
        //            await Application.Current.MainPage.DisplayAlert("Error", "No se pudo agregar el producto.", "Aceptar");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await Application.Current.MainPage.DisplayAlert("Error", $"ERROR al grabar: {ex.Message}", "Aceptar");
        //    }
        //}

        //[RelayCommand]
        //private async Task CargarImagen() // Comando para cargar la imagen
        //{
        //    var result = await FilePicker.PickAsync(new PickOptions
        //    {
        //        FileTypes = FilePickerFileType.Images,
        //        PickerTitle = "Seleccionar imagen"
        //    });

        //    if (result != null)
        //    {
        //        // Lee la imagen seleccionada como bytes
        //        using var stream = await result.OpenReadAsync();
        //        using var memoryStream = new MemoryStream();
        //        await stream.CopyToAsync(memoryStream);
        //        imagen = memoryStream.ToArray(); // Guardar la imagen como byte array
        //    }
        //}

        //private List<Valor> GetCategoriasValues()
        //{
        //    // TODO: reemplazar por lista de valores de la base de datos
        //    var categoriasValues = new List<Valor>()
        //{
        //    new Valor { Key = 1, Value = "Alimentos" },
        //    new Valor { Key = 2, Value = "Bebidas" },
        //    new Valor { Key = 3, Value = "Limpieza" },
        //    new Valor { Key = 4, Value = "Higiene" },
        //    new Valor { Key = 5, Value = "Varios" }
        //};

        //    return categoriasValues;
        //}

        [ObservableProperty] private string nombre;
        [ObservableProperty] private string descripción;
        [ObservableProperty] private int stock;
        [ObservableProperty] private decimal precio;
       // [ObservableProperty] private byte[] imagen;
        //[ObservableProperty] private IFormFile image;
        //[ObservableProperty] List<Valor> listaCategorias;
        //[ObservableProperty] private Valor categoriaSeleccionada;

        [ObservableProperty]
        CrearProductoDto producto = new CrearProductoDto();

        [ObservableProperty]
        ImageSource _ModificarImagen;



        public ProductoAgregarViewModel()
        {
            Title = Constants.AppName;
            //listaCategorias = GetCategoriasValues();
        }


        [RelayCommand]
        private async Task Cancelar()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        //[RelayCommand]
        //private async Task GrabarProducto()
        //{
        //    if (!IsBusy)
        //    {
        //        IsBusy = true;
        //        //if(producto.Imagen==null || producto.Nombre==null || producto.Descripcion==null || producto.Precio==null || producto.Stock==null) await App.Current.MainPage.DisplayAlert("Error!", "Completar todos los campos", "Ok");

        //        if (string.IsNullOrWhiteSpace(producto.Nombre) || string.IsNullOrWhiteSpace(producto.Descripcion) || producto.Precio <= 0 || producto.Stock <= 0 || producto.Imagen == null)
        //        {
        //            await App.Current.MainPage.DisplayAlert("Error!", "Completar todos los campos", "Ok");
        //            return;
        //        }


        //        ////var response= ApiService.PostProductosAsync(producto);
        //        //var response = await ApiService.PostProductosAsync(producto);


        //        //if (response != null) 
        //        //{
        //        //    IsBusy = false;
        //        //    await App.Current.MainPage.DisplayAlert("Bien!", $"Producto creado", "Ok");
        //        //}
        //        //else
        //        //{
        //        //    IsBusy = false;
        //        //    await App.Current.MainPage.DisplayAlert("Error!",$"{response.Exception}", "Ok");

        //        //}
        //        try
        //        {
        //            var response = await ApiService.PostProductosAsync(producto);

        //            if (response)
        //            {
        //                IsBusy = false;
        //                await App.Current.MainPage.DisplayAlert("Bien!", "Producto creado", "Ok");
        //            }
        //            else
        //            {
        //                IsBusy = false;
        //                await App.Current.MainPage.DisplayAlert("Error!", "No se pudo crear el producto", "Ok");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            IsBusy = false;
        //            await App.Current.MainPage.DisplayAlert("Error!", ex.Message, "Ok");
        //        }


        //    }
        //}

        // Definir la colección observable en el ViewModel


        //profe

        [ObservableProperty] private string rutaImagen;
        [ObservableProperty] private FileResult imagen;

        //-----


        public ObservableCollection<CrearProductoDto> Productocrear { get; set; } = new ObservableCollection<CrearProductoDto>();
        public ObservableCollection<Producto> Productos { get; set; } = new ObservableCollection<Producto>();


        //----este es mio toma los datos pero no la imagen 
        //[RelayCommand]
        //private async Task GrabarProducto()
        //{
        //    //Application.Current.MainPage.DisplayAlert("Falla", "Agregando producto");
        //    if (!IsBusy)
        //    {
        //        IsBusy = true;


        //        //var _producto = new CrearProductoDto
        //        //{


        //        //    Nombre = this.Nombre,
        //        //    Descripción = this.Descripción,
        //        //    Precio = this.Precio,
        //        //    Stock = this.Stock


        //        //};

        //        //if (imagen != null && imagen.Length > 0)
        //        //{
        //        //    // Crear el IFormFile a partir del array de bytes de la imagen
        //        //    using var memoryStream = new MemoryStream(imagen);
        //        //    _producto.Imagen = new FormFile(memoryStream, 0, imagen.Length, "imagen", "producto.jpg")
        //        //    {
        //        //        Headers = new HeaderDictionary(),
        //        //        ContentType = producto.Imagen.ContentType ?? "application/octet-stream"// O el tipo adecuado según la imagen
        //        //    };
        //        //}

        //        //try
        //        //{
        //        //    await ApiService.PostProductosAsync(_producto);

        //        //    await Application.Current.MainPage.DisplayAlert("Exito", "Se nuevo Producto.", "Aceptar");
        //        //}
        //        //catch (Exception ex)
        //        //{
        //        //    await Application.Current.MainPage.DisplayAlert("Error", "ERROR al grabar.", "Aceptar");
        //        //}

        //        //await Application.Current.MainPage.Navigation.PopAsync();




        //        // Asignar valores a producto desde las propiedades del ViewModel
        //        producto.Nombre = nombre;
        //        producto.Descripción = descripción;
        //        producto.Precio = precio;
        //        producto.Stock = stock;
        //        // Asegúrate de que imagen está correctamente establecida


        //        // Validar los campos del producto
        //        if (string.IsNullOrWhiteSpace(producto.Nombre) ||
        //            string.IsNullOrWhiteSpace(producto.Descripción) ||
        //            producto.Precio <= 0 ||
        //            producto.Stock <= 0 )
        //            //||
        //            //producto.Imagen == null
        //        {
        //            await App.Current.MainPage.DisplayAlert("Error!", "Completar todos los campos", "Ok");
        //            IsBusy = false;
        //            return;
        //        }


        //        var response = await ApiService.PostProductosAsync(producto);

        //        if (response)
        //        {
        //            await App.Current.MainPage.DisplayAlert("Bien!", "Producto creado", "Ok");
        //        }
        //        else
        //        {
        //            await App.Current.MainPage.DisplayAlert("Error!", "No se pudo crear el producto", "Ok");
        //        }

        //        //if (response != null)
        //        //{
        //        //    productos.Add(response);  // Agregar producto a la lista local
        //        //    await App.Current.MainPage.DisplayAlert("Éxito", "Producto creado correctamente.", "Ok");

        //        //    // Reiniciar valores para un nuevo producto
        //        //    nombre = string.Empty;
        //        //    descripción = string.Empty;
        //        //    precio = 0;
        //        //    stock = 0;
        //        //    imagen = null;
        //        //    ModificarImagen = null;  // Resetear la imagen en la interfaz

        //        //    await Application.Current.MainPage.Navigation.PopAsync(); // Volver a la página anterior
        //        //}
        //        //else
        //        //{
        //        //    await App.Current.MainPage.DisplayAlert("Error", "No se pudo crear el producto", "Ok");
        //        //}


        //    }


        //    //task si es bool 
        //    //try
        //    //{
        //    //    var response = await ApiService.PostProductosAsync(producto);

        //    //    if (response)
        //    //    {
        //    //        await App.Current.MainPage.DisplayAlert("Bien!", "Producto creado", "Ok");
        //    //    }
        //    //    else
        //    //    {
        //    //        await App.Current.MainPage.DisplayAlert("Error!", "No se pudo crear el producto", "Ok");
        //    //    }


        //    //var response = await ApiService.PostProductosAsync(producto);

        //    //if (response != null)
        //    //{
        //    //    IsBusy = false;
        //    //    await App.Current.MainPage.DisplayAlert("Bien!", $"Producto creado", "Ok");
        //    //}
        //    //else
        //    //{
        //    //    IsBusy = false;
        //    //    await App.Current.MainPage.DisplayAlert("Error!", $"{response.Exception}", "Ok");

        //    //}



        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    await App.Current.MainPage.DisplayAlert("Error!", ex.Message, "Ok");
        //    //}
        //    //finally
        //    //{
        //    //    IsBusy = false;
        //    //}

        //}
        //---------

        //[RelayCommand]
        //public async Task BuscarImagenAsync()
        //{
        //    if (!IsBusy)
        //    {
        //        try
        //        {
        //            IsBusy = true;

        //            FileResult? result = await FilePicker.PickAsync(new PickOptions
        //            {
        //                PickerTitle = "Selecciona una imagen"
        //            });

        //            if (result != null)
        //            {
        //                // Asignas el resultado al modelo
        //                producto.Imagen = result;

        //                var stream = await result.OpenReadAsync();

        //                ModificarImagen = ImageSource.FromStream(() => stream); // Asegura que esto está actualizando la UI
        //                Application.Current.MainPage.BindingContext = this;
        //                // Aquí se fuerza la actualización del BindingContext si no se refleja automáticamente
        //                OnPropertyChanged(nameof(ModificarImagen));
        //            }
        //            else
        //            {
        //                await App.Current.MainPage.DisplayAlert("Error!", "Imagen no cargada", "Ok");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            await App.Current.MainPage.DisplayAlert("Error!", ex.Message, "Ok");
        //        }
        //        finally
        //        {
        //            IsBusy = false;
        //        }
        //    }
        //}


        //este es el que funciona del mio 
        //[RelayCommand]
        //public async Task BuscarImagenAsync()
        //{
        //    if (!IsBusy)
        //    {
        //        try
        //        {
        //            IsBusy = true;

        //            // Seleccionar la imagen usando el FilePicker
        //            FileResult? result = await FilePicker.PickAsync(new PickOptions
        //            {
        //                PickerTitle = "Selecciona una imagen"
        //            });

        //            if (result != null)
        //            {
        //                // Abre el stream de la imagen seleccionada
        //                var stream = await result.OpenReadAsync();

        //                // Crea un objeto MemoryStream para convertirlo a IFormFile
        //                using var memoryStream = new MemoryStream();
        //                await stream.CopyToAsync(memoryStream);

        //                // Crear el objeto IFormFile (usando el constructor correcto)
        //                producto.Imagen = new FormFile(memoryStream, 0, memoryStream.Length, result.FileName, result.FileName)
        //                {
        //                    Headers = new HeaderDictionary(),
        //                    ContentType = result.ContentType ?? "application/octet-stream"  // Verifica y asigna el ContentType
        //                };

        //                // Actualiza la imagen en la interfaz (UI)
        //                ModificarImagen = ImageSource.FromStream(() => new MemoryStream(memoryStream.ToArray()));
        //                Application.Current.MainPage.BindingContext = this;
        //                OnPropertyChanged(nameof(ModificarImagen));
        //            }
        //            else
        //            {
        //                await App.Current.MainPage.DisplayAlert("Error!", "Imagen no cargada", "Ok");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            await App.Current.MainPage.DisplayAlert("Error!", ex.Message, "Ok");
        //        }
        //        finally
        //        {
        //            IsBusy = false;
        //        }
        //    }
        //}


        //profe

        [RelayCommand]
        private async Task GrabarProducto()
        {

            // validar datos
            if (string.IsNullOrEmpty(this.Nombre) || string.IsNullOrEmpty(this.Descripción) || this.Stock <= 0 || this.Precio <= 0 )
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Datos incompletos. Verifique!", "Aceptar");
                return;
            }


            var registro = new Producto
            {
                Nombre = this.Nombre,
                Descripción = this.Descripción,
                Stock = this.Stock,
                Precio = Convert.ToDecimal(this.Precio),
                ImagenUrl = this.rutaImagen,
                Imagen = this.imagen
            };


            try
            {
                await ApiService.AgregarProductoConImagen(registro);

                await Application.Current.MainPage.DisplayAlert("Exito", "Se nuevo Producto.", "Aceptar");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "ERROR al grabar.", "Aceptar");
            }

            //await Application.Current.MainPage.Navigation.PopAsync();
            await Application.Current.MainPage.Navigation.PushAsync(new ProductoListaPage(new ProductoListaViewModel()));

        }

        [RelayCommand]
        private async Task FotoGaleria()
        {
            try
            {
                // tomar foto y guardar en variable 
                if (MediaPicker.Default.IsCaptureSupported)
                {
                    FileResult foto = await MediaPicker.PickPhotoAsync();

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

        [RelayCommand]
        private async Task TomarFoto()
        {
            try
            {
                // tomar foto y guardar en variable 
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


        //-----------------------





        //[RelayCommand]
        //public async Task AgregarProducto()
        //{
        //    // Validaciones previas
        //    if (string.IsNullOrEmpty(this.Nombre))
        //    {
        //        await Application.Current.MainPage.DisplayAlert("Error", "El campo Nombre es obligatorio.", "Aceptar");
        //        return;
        //    }

        //    if (string.IsNullOrEmpty(this.Descripción))
        //    {
        //        await Application.Current.MainPage.DisplayAlert("Error", "El campo Descripción es obligatorio.", "Aceptar");
        //        return;
        //    }

        //    // Crear la instancia del DTO con los datos del producto
        //    var productoDto = new CrearProductoDto
        //    {
        //        Nombre = this.Nombre,
        //        Descripcion = this.Descripción,
        //        Stock = this.Stock,
        //        Precio = Convert.ToDecimal(this.Precio),
        //        CategoriaId = this.CategoriaSeleccionada?.Key ?? 0,
        //        Imagen = imagenBytes // Enviar la imagen seleccionada como byte[]
        //    };

        //    try
        //    {
        //        // Llamar al método para agregar el producto
        //        var resultado = await ApiService.PostProductosAsync(productoDto);

        //        if (resultado != null)
        //        {
        //            // Mostrar mensaje de éxito
        //            await Application.Current.MainPage.DisplayAlert("Éxito", "Producto agregado correctamente.", "Aceptar");
        //            await Application.Current.MainPage.Navigation.PopAsync(); // Regresar a la página anterior o lista de productos
        //        }
        //        else
        //        {
        //            // Mostrar error si el resultado es nulo
        //            await Application.Current.MainPage.DisplayAlert("Error", "No se pudo agregar el producto.", "Aceptar");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Mostrar cualquier excepción ocurrida
        //        await Application.Current.MainPage.DisplayAlert("Error", $"Ocurrió un error al agregar el producto: {ex.Message}", "Aceptar");
        //    }
        //}





        //[RelayCommand]
        //private async Task CargarImagen() // Comando para cargar la imagen
        //{
        //    var result = await FilePicker.PickAsync(new PickOptions
        //    {
        //        PickerTitle = "Seleccionar imagen"
        //    });

        //    if (result != null)
        //    {
        //        // Lee la imagen seleccionada como bytes
        //        using var stream = await result.OpenReadAsync();
        //        using var memoryStream = new MemoryStream();
        //        await stream.CopyToAsync(memoryStream);
        //        imagen = memoryStream.ToArray(); // Guardar la imagen como byte array
        //    }
        //    else
        //    {
        //        await Application.Current.MainPage.DisplayAlert("Error", "No se seleccionó ninguna imagen.", "Aceptar");
        //    }
        //}
        //private byte[] imagenBytes;
        //private ImageSource modificarImagen;
        //public ImageSource ModificarImagen
        //{
        //    get => modificarImagen;
        //    set
        //    {
        //        modificarImagen = value;
        //        OnPropertyChanged(nameof(ModificarImagen)); // Notificar el cambio de propiedad
        //    }
        //}

        //[RelayCommand]
        //public async Task SeleccionarImagen()
        //{
        //    IsBusy = true; // Mostrar un indicador de carga si es necesario

        //    try
        //    {
        //        // Usar FilePicker para seleccionar una imagen
        //        FileResult? result = await FilePicker.PickAsync(new PickOptions
        //        {
        //            PickerTitle = "Selecciona una imagen"
        //        });

        //        if (result != null)
        //        {
        //            // Asignar el resultado al modelo (aquí puedes almacenar la imagen)
        //            var stream = await result.OpenReadAsync();

        //            // Actualizar la propiedad que contiene la imagen
        //            ModificarImagen = ImageSource.FromStream(() => stream); // Carga la imagen en la UI
        //        }
        //        else
        //        {
        //            await Application.Current.MainPage.DisplayAlert("Error", "Imagen no cargada", "Ok");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await Application.Current.MainPage.DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "Ok");
        //    }
        //    finally
        //    {
        //        IsBusy = false; // Ocultar el indicador de carga
        //    }
        //}
        //private ImageSource modificarImagen;

        //public ImageSource ModificarImagen
        //{
        //    get => modificarImagen;
        //    set
        //    {
        //        modificarImagen = value;
        //        OnPropertyChanged(nameof(ModificarImagen)); // Notificar el cambio de propiedad
        //    }
        //}

        //[RelayCommand]
        //private async Task SeleccionarImagenAsync(CrearProductoDto producto)
        //{
        //    try
        //    {
        //        // Usar FilePicker para seleccionar una imagen
        //        FileResult? result = await FilePicker.PickAsync(new PickOptions
        //        {
        //            PickerTitle = "Selecciona una imagen"
        //        });

        //        if (result != null)
        //        {
        //            // Crear un Stream para la imagen seleccionada
        //            using var stream = await result.OpenReadAsync();

        //            // Copiar el stream en un MemoryStream para poder usarlo varias veces
        //            var memoryStream = new MemoryStream();
        //            await stream.CopyToAsync(memoryStream);
        //            memoryStream.Position = 0; // Reiniciar la posición del MemoryStream

        //            // Crear un IFormFile a partir del MemoryStream
        //            producto.Imagen = new FormFile(memoryStream, 0, memoryStream.Length, result.FileName, result.FileName)
        //            {
        //                Headers = new HeaderDictionary(),
        //                ContentType = result.ContentType
        //            };

        //            // Actualizar la imagen en la interfaz de usuario
        //            ModificarImagen = ImageSource.FromStream(() => new MemoryStream(memoryStream.ToArray()));
        //        }
        //        else
        //        {
        //            await Application.Current.MainPage.DisplayAlert("Error", "Imagen no cargada", "Ok");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await Application.Current.MainPage.DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "Ok");
        //    }
        //}

        //[RelayCommand]
        //public async Task AgregarProducto()
        //{
        //    var nuevoProducto = new CrearProductoDto
        //    {
        //        Nombre = this.Nombre,
        //        Descripcion = this.Descripción,
        //        Stock = this.Stock,
        //        Precio = Convert.ToDecimal(this.Precio),
        //        CategoriaId = this.CategoriaSeleccionada?.Key ?? 0

        //    };

        //    await SeleccionarImagenAsync(nuevoProducto); // Seleccionar la imagen y asignarla al producto

        //    try
        //    {
        //        var productoCreado = await ApiService.PostProductosAsync(nuevoProducto);
        //        await Application.Current.MainPage.DisplayAlert("Éxito", "Producto agregado correctamente", "Ok");
        //    }
        //    catch (Exception ex)
        //    {
        //        await Application.Current.MainPage.DisplayAlert("Error", $"Ocurrió un error al agregar el producto: {ex.Message}", "Ok");
        //    }
        //}



        //private List<Valor> GetCategoriasValues()
        //{
        //    // TODO: reemplazar por lista de valores de la base de datos
        //    var categoriasValues = new List<Valor>()
        //{
        //    new Valor { Key = 1, Value = "Alimentos" },
        //    new Valor { Key = 2, Value = "Bebidas" },
        //    new Valor { Key = 3, Value = "Limpieza" },
        //    new Valor { Key = 4, Value = "Higiene" },
        //    new Valor { Key = 5, Value = "Varios" }
        //};

        //    return categoriasValues;
        //}
    }

}

