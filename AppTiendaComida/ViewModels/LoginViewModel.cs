using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Android.Net;
using AppTiendaComida.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AppTiendaComida.Models;
using AppTiendaComida.Services;
using AppTiendaComida.Views;

namespace AppTiendaComida.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
            Title = Constants.AppName;

        }

        [ObservableProperty] private string email = string.Empty;
        [ObservableProperty] private string password = string.Empty;
        [ObservableProperty] private string message = string.Empty;

        [RelayCommand]
        public async Task LoginAsync2()
        {
             //await Application.Current.MainPage.DisplayAlert("Login", "Login", "Ok");

           // await Application.Current.MainPage.Navigation.PushAsync(new ProductoListaPage(new ProductoListaViewModel()));
        }

        //[RelayCommand]
        //public async Task LoginAsyn()
        //{
        //    if (!IsBusy)
        //    {
        //        IsBusy = true;

        //        if (Email != string.Empty && Password != String.Empty)
        //        {
        //            var apiClient = new ApiService();
        //            LoginResponseDto login = await apiClient.ValidarLogin(Email, Password);



        //            if (login != null)
        //            {
        //                //Application.Current.MainPage = new NavigationPage(new ProductoListaPage(new ProductoListaViewModel()));

        //                // TODO: recuperar datos de usuario login
        //                Transport.UsuarioId = login.UsuarioId;
        //                Transport.Usuario = login.Usuario;
        //                Transport.Rol = login.Rol;
        //                Transport.CorreoUsuario = login.Correo;
        //                Transport.Nombre = login.Nombre;

        //            }
        //            else
        //            {
        //                await Application.Current.MainPage.DisplayAlert("Atención", "Las credenciales ingresadas no son válidas", "Aceptar");
        //            }
        //        }
        //        else
        //        {
        //            await Application.Current.MainPage.DisplayAlert("Atención", "Las credenciales son Obligatorias. Verifique!", "Aceptar");
        //        }

        //        IsBusy = false;
        //    }

        //}
        //[RelayCommand]
        //public async Task Login()
        //{
        //    if (!IsBusy)
        //    {
        //        IsBusy = true;

        //        if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
        //        {
        //            var apiClient = new ApiService();

        //            try
        //            {
        //                // Llamada al servicio API
        //                LoginResponseDto login = await apiClient.ValidarLogin(Email, Password);

        //                if (login != null)
        //                {
        //                    Message = "Ingreso exitoso"; // Asignar mensaje exitoso
        //                    await Application.Current.MainPage.DisplayAlert("Éxito", "Ingreso exitoso", "Aceptar");
        //                }
        //                else
        //                {
        //                    Message = "Credenciales inválidas"; // Asignar mensaje de error
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Message = $"Error al iniciar sesión: {ex.Message}";
        //            }
        //        }
        //        else
        //        {
        //            Message = "Las credenciales son obligatorias. Verifique!";
        //        }

        //        IsBusy = false;
        //    }
        //}
        [RelayCommand]
        public async Task Login()
        {
            if (!IsBusy)
            {
                IsBusy = true;

                if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
                {
                    var apiClient = new ApiService();

                    try
                    {
                        // Llamada al servicio API
                        LoginResponseDto login = await apiClient.ValidarLogin(Email, Password);

                        //if (login != null)
                        //{
                        //    Message = "Ingreso exitoso"; // Asignar mensaje exitoso
                        //    await Application.Current.MainPage.DisplayAlert("Éxito", "Ingreso exitoso", "Aceptar");

                        //}
                        


                        if (login != null)
                        {
                            Application.Current.MainPage = new NavigationPage(new ProductoListaPage(new ProductoListaViewModel()));

                            // TODO: recuperar datos de usuario login
                            Transport.UsuarioId = login.UsuarioId;
                            Transport.Usuario = login.Usuario;
                            Transport.Rol = login.Rol;
                            Transport.CorreoUsuario = login.Correo;
                            Transport.Nombre = login.Nombre;
                        }

                    }
                    catch (Exception ex)
                    {
                        // Mostrar mensaje genérico de error
                        Message = "Error de credenciales. Vuelve a ingresar.";
                        await Application.Current.MainPage.DisplayAlert("Error", Message, "Aceptar");
                    }
                }
                else
                {
                    Message = "Las credenciales son obligatorias. Verifique!";
                    await Application.Current.MainPage.DisplayAlert("Atención", Message, "Aceptar");
                }

                IsBusy = false;
            }
        }

    }
}
