using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppTiendaComida.Models;
using AppTiendaComida.Services;
using AppTiendaComida.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;



namespace AppTiendaComida.ViewModels
{
     public partial class UsuarioModificarViewModel : BaseViewModel
    {
        [ObservableProperty] private string nombre;
        [ObservableProperty] private string usuario1;  // El nombre de usuario
        [ObservableProperty] private string telefono;
        [ObservableProperty] private string correo;
        [ObservableProperty] private string contraseña;
        [ObservableProperty] private string rol;


        //[ObservableProperty]
        //Usuario _Usuario;
        public UsuarioModificarViewModel()
        {
            Title = "Editar usuario";
        }
        
        // Propiedad observable que contiene el producto actual
        [ObservableProperty]
        private Usuario usuario;
        


        public UsuarioModificarViewModel(Usuario usuarioSeleccionado)
        {
            Usuario = usuarioSeleccionado;

            // Inicializa las propiedades del ViewModel a partir del objeto Usuario
            if (Usuario != null)
            {
                Nombre = Usuario.Nombre;
                Usuario1 = Usuario.Usuario1;
                Telefono = Usuario.Telefono;
                Correo = Usuario.Correo;
                Contraseña = Usuario.Contraseña;
                Rol = Usuario.Rol;
            }

            Title = "Editar usuario";

        }


        [RelayCommand]
        public async Task EditarUserAsync()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;

                   

                    var result = await ApiService.UpdateUserAsync(Usuario.UsuarioId, Usuario);
                    if (result != null)
                    {
                        IsBusy = false;
                        await App.Current.MainPage.DisplayAlert("Respuesta!", "Usuario modificado correctamente", "Ok");
                        await Application.Current.MainPage.Navigation.PushAsync(new UsuarioPage(new UsuariosViewModel()));

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

                await Application.Current.MainPage.Navigation.PushAsync(new UsuarioPage(new UsuariosViewModel()));

            }
        }

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

        //// Constructor
        //public UsuarioModificarViewModel(Usuario usuarioDetalles)
        //{
        //    UsuarioDetalles = usuarioDetalles;
        //}

        // Implementación de INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [RelayCommand]
        private async Task Volver()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

    }
}
