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
        //[ObservableProperty]
        //private ObservableCollection<Usuario> _usuarios;
        public ObservableCollection<Usuario> Usuarios { get; } = new ObservableCollection<Usuario>();


        [ObservableProperty]
        private Usuario _usuarioSeleccionado;

        [ObservableProperty]
        private bool isRefreshing;

        public ICommand CargarUsuariosCommand { get; }
        public UsuariosViewModel()
        {
            Title = "Lista de Usuarios"; // Título de la página

            // Inicializar la lista de usuarios
            //CargarUsuariosCommand = new RelayCommand(async () => await GetUsuario());
            Task.Run(async () => { await GetUsuario(); }).Wait();
            //Task.Run(async () => await GetUsuario());

            CargarUsuarios();
        }

        
        [RelayCommand]
        private async Task GetUsuario()
        {
            IsBusy = IsRefreshing = true;

            try
            {
                // Limpia la colección existente antes de cargar nuevos datos
                Usuarios.Clear();

                var usuarios = await ApiService.GetUsuario();

                

                if (usuarios != null)
                {
                    //Usuarios = new ObservableCollection<Usuario>(usuarios);

                    //Usuarios.Clear(); // Limpia la colección existente
                    foreach (var usuario in usuarios)
                    {
                        Usuarios.Add(usuario); // Agrega cada usuario a la colección
                    }
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

                    if (UsuarioSeleccionado != null)
                    {
                        // Navega a la página de detalles con la información del usuario obtenido
                        await Application.Current.MainPage.Navigation.PushAsync(new UsuarioDetallePage(UsuarioSeleccionado));
                    }
                }
                catch (Exception ex)
                {
                    // Manejar errores si es necesario
                    await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }


        // Comando para agregar un nuevo usuario
        [RelayCommand]
        private async Task NuevoUsuario()
        {
            // await Application.Current.MainPage.Navigation.PushAsync(new UsuarioAgregarPage(new UsuarioAgregarViewModel(new ApiService())));
            await Application.Current.MainPage.Navigation.PushAsync(new UsuarioAgregarPage(new UsuarioAgregarViewModel(new ApiService())));
        }

        partial void OnUsuarioSeleccionadoChanged(Usuario value)
        {
            if (value != null)
            {
                // Navegar a los detalles del usuario seleccionado
                GoToDetalleCommand.Execute(null);
            }
        }

        [RelayCommand]
        private async Task MenuProducto()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new InicioPage());
        }



        //prueba de carga 
        // Método para cargar usuarios
        public async Task CargarUsuarios()
        {
            IsBusy = true;
            try
            {
                var usuarios = await ApiService.GetUsuario(); // Método que hace la llamada a la API para obtener usuarios
                Usuarios.Clear(); // Limpia la colección actual
                foreach (var usuario in usuarios)
                {
                    Usuarios.Add(usuario); // Agrega los nuevos usuarios a la colección
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"No se pudo cargar la lista de usuarios: {ex.Message}", "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }


    }
}
