using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppTiendaComida.Models;
using AppTiendaComida.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AppTiendaComida.ViewModels
{
     public partial class UsuarioModificarViewModel : BaseViewModel
    {
        [ObservableProperty]
        Usuario _Usuario;
        public UsuarioModificarViewModel()
        {
            Title = "Editar usuario";
        }

        [RelayCommand]
        public async Task EditarUserAsync()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = false;
                    var result = await ApiService.UpdateUserAsync(Usuario.UsuarioId, Usuario);
                    if (result != null)
                    {
                        IsBusy = false;
                        await App.Current.MainPage.DisplayAlert("Respuesta!", "Usuario modificado correctamente", "Ok");
                    }
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Error!", "ex.Message", "Ok");

                }
                finally
                {
                    IsBusy = false;
                }
            }
        }
    }
}
