using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppTiendaComida.Models;
using AppTiendaComida.Services;
using AppTiendaComida.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace AppTiendaComida.ViewModels
{
    public partial class UsuarioAgregarViewModel : BaseViewModel
    {
        private readonly ApiService _apiService; // Inyección del ApiService
        public ObservableCollection<Usuario> Usuarios { get; } = new ObservableCollection<Usuario>();

        [ObservableProperty] private string nombre;
        [ObservableProperty] private string email;
        [ObservableProperty] private string direccion;
        [ObservableProperty] private string rol;
        [ObservableProperty] private string contraseña;
        [ObservableProperty] private string telefono;
        [ObservableProperty] private string usuario1;

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
            var nuevoUsuario = new Usuario
            {
                Nombre = this.nombre,
                Correo = this.email,
                Telefono = this.telefono,
                Rol = this.rol,
                Contraseña = this.contraseña,
                Usuario1 = this.usuario1
            };

            try
            {
                await ApiService.AgregarUsuario(nuevoUsuario);

                await Application.Current.MainPage.DisplayAlert("Exito", "Se nuevo Producto.", "Aceptar");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "ERROR al grabar.", "Aceptar");
            }

            await Application.Current.MainPage.Navigation.PopAsync();


        }
    }
}
