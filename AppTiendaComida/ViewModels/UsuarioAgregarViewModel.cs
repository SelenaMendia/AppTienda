using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class UsuarioAgregarViewModel : BaseViewModel
    {
        private readonly ApiService _apiService; // Inyección del ApiService
        public ObservableCollection<Usuario> Usuarios { get; } = new ObservableCollection<Usuario>();

        public ObservableCollection<UsuarioListaDTO> Usuarioscrear { get; set; } = new ObservableCollection<UsuarioListaDTO>();

        [ObservableProperty] private string nombre;
        [ObservableProperty] private string correo;
        [ObservableProperty] private string rol;
        [ObservableProperty] private string contraseña;
        [ObservableProperty] private string telefono;
        [ObservableProperty] private string usuario;

        public UsuarioAgregarViewModel(ApiService apiService)
        {
            _apiService = apiService; // Asigna el ApiService recibido por el constructor
            Title = Constants.AppName;
        }

        [RelayCommand]
        private async Task CancelarUsuario()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        [RelayCommand]
        private async Task GrabarUsuario()
        {
            var nuevoUsuario = new UsuarioListaDTO
            {
                Nombre = this.nombre,
                Correo = this.correo,
                Telefono = this.telefono,
                Rol = this.rol,
                Contraseña = this.contraseña,
                Usuario = this.usuario
            };

            try
            {
                await ApiService.AgregarUsuario(nuevoUsuario);

                await Application.Current.MainPage.DisplayAlert("Exito", "Se agrego nuevo Usuario.", "Aceptar");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Error al agregar.", "Aceptar");
            }

            //await Application.Current.MainPage.Navigation.PopAsync();
            await Application.Current.MainPage.Navigation.PushAsync(new UsuarioPage(new UsuariosViewModel()));




        }
    }
}
