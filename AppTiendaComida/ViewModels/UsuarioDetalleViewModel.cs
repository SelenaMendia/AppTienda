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

        public UsuarioDetalleViewModel(int usuarioId)
        {
            _ = GetUsuarioPorId(usuarioId); // Obtén el usuario por ID
        }
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

        //[RelayCommand]
        //private async Task ModificarUsuario()
        //{
        //    // Pasar el usuario seleccionado al modificar
        //    await Application.Current.MainPage.Navigation.PushAsync(new UsuarioModificarPage(_usuarioSeleccionado));
        //}
    }
}
