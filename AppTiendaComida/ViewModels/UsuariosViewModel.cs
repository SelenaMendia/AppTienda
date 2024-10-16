using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppTiendaComida.Models;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using AppTiendaComida.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using AppTiendaComida.Models.DTO;
using AppTiendaComida.Views;

namespace AppTiendaComida.ViewModels
{
    public partial class UsuariosViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<Usuario> _usuarios;

        [ObservableProperty]
        private Usuario _usuarioSeleccionado;

        [ObservableProperty]
        private bool isRefreshing;


        public UsuariosViewModel()
        {
            Title = "Lista de Usuarios"; // Título de la página

            // Inicializar la lista de usuarios
            Task.Run(async () => { await GetUsuario(); }).Wait();
        }

        [RelayCommand]
        private async Task GetUsuario()
        {
            IsBusy = IsRefreshing = true;

            try
            {
                var usuarios = await ApiService.GetUsuario();

                if (usuarios != null)
                {
                    Usuarios = new ObservableCollection<Usuario>(usuarios);
                }
            }
            catch (Exception ex)
            {
                // Manejar errores
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = IsRefreshing = false;
            }
        }

        partial void OnUsuarioSeleccionadoChanged(Usuario value)
        {
            if (value != null)
            {
                // Llama al comando para obtener los detalles del usuario
                GoToDetalleCommand.Execute(null);
            }
        }

        // Comando para navegar a la página de detalles del usuario
        

        [RelayCommand]
        private async Task GoToDetalle()
        {
            if (UsuarioSeleccionado != null)
            {
                try
                {
                    // Cambia a un método que obtenga detalles de usuario
                    var usuarioDetalles = await ApiService.GetUsuarioPorId(UsuarioSeleccionado.UsuarioId);

                    if (usuarioDetalles != null)
                    {
                        // Navega a la página de detalles con la información del usuario obtenido
                        await Application.Current.MainPage.Navigation.PushAsync(new UsuarioDetallePage(usuarioDetalles));
                    }
                }
                catch (Exception ex)
                {
                    // Manejar errores si es necesario
                    await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }


        //// Comando para agregar un nuevo usuario
        //[RelayCommand]
        //private async Task NuevoUsuario()
        //{
        //    await Application.Current.MainPage.Navigation.PushAsync(new UsuarioAgregarPage());
        //}

        [RelayCommand]
        private async Task MenuProducto()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new InicioPage());
        }


    }
}
