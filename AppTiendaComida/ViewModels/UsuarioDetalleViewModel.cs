using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppTiendaComida.Models;
using AppTiendaComida.Models.DTO;
using AppTiendaComida.Services;
using AppTiendaComida.Utils;
using AppTiendaComida.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AppTiendaComida.ViewModels
{
    [QueryProperty(nameof(UsuarioId), "UsuarioId")]
    public partial class UsuarioDetalleViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Usuario> _usuarios;

        [ObservableProperty]
        private Usuario _usuarioSeleccionado;



        [ObservableProperty]
        private bool isBusy; // Indica si el ViewModel está ocupado

        [ObservableProperty]
        private string title; // Propiedad para el título

        [ObservableProperty]
        private bool isRefreshing;

        private int usuarioId; // Almacena el ID del usuario

        //public UsuarioDetalleViewModel(int usuarioId)
        //{
        //    _ = GetUsuarioPorId(usuarioId); // Obtén el usuario por ID
        //}
        public int UsuarioId
        {
            get => usuarioId;
            set
            {
                usuarioId = value;
                _ = GetUsuarioPorId(usuarioId); // Llama a obtener el usuario por ID de manera asíncrona
            }
        }

        public UsuarioDetalleViewModel()
        {
            Title = Constants.AppName; // Establece el título
        }

        // Constructor
        private Usuario _usuarioDetalles;

        public Usuario UsuarioDetalles
        {
            get => _usuarioDetalles;
            set
            {
                _usuarioDetalles = value;
                OnPropertyChanged(nameof(UsuarioDetalles));
            }
        }

        // Constructor
        public UsuarioDetalleViewModel(Usuario usuarioDetalles)
        {
            UsuarioDetalles = usuarioDetalles;
        }

        // Implementación de INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        private async Task GetUsuarioPorId(int id)
        {
            IsBusy = true; // Indica que estamos ocupados
            try
            {
                var usuario = await ApiService.GetUsuarioPorId(id); // Asegúrate de que retorna UsuarioListaDTO

                if (usuario != null)
                {
                    _usuarioSeleccionado = usuario;
                    // Asigna el usuario obtenido
                }
                else
                {
                    Console.WriteLine("Usuario no encontrado.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar el usuario: {ex.Message}");
            }
            finally
            {
                IsBusy = false; // Finaliza la operación
            }
        }


        [RelayCommand]
        private async Task Volver()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }


        [RelayCommand]
        public async Task DeleteUsuarioAsync()
        {
            if (UsuarioSeleccionado?.UsuarioId > 0) // Verifica si hay un usuario seleccionado y su ID es válido
            {
                try
                {
                    IsBusy = true;
                    bool confirmacion = await Application.Current.MainPage.DisplayAlert(
                        "Confirmar",
                        "¿Estás seguro de que deseas borrar el usuario?",
                        "Sí",
                        "No");

                    if (confirmacion)
                    {
                        string resultado = await ApiService.BorrarUsuario(UsuarioSeleccionado.UsuarioId);
                        await Application.Current.MainPage.DisplayAlert("Resultado", resultado, "Ok"); // Muestra el resultado
                                                                                                       //await Application.Current.MainPage.Navigation.PopAsync(); // Navega hacia atrás
                        await Application.Current.MainPage.Navigation.PushAsync(new UsuarioPage(new UsuariosViewModel()));

                    }
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error!", $"Ha ocurrido un error: {ex.Message}", "Ok");
                }
                finally
                {
                    IsBusy = false;
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error!", "No hay un usuario seleccionado válido", "Ok");
            }
        }




        [RelayCommand]
        private async Task ModificarProducto()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new UsuarioModificarPage(UsuarioSeleccionado));

            
        }
    }
}
