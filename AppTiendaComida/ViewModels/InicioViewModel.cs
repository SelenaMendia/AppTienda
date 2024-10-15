using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppTiendaComida.Services;
using AppTiendaComida.Views;
using CommunityToolkit.Mvvm.Input;

namespace AppTiendaComida.ViewModels
{
    public partial class InicioViewModel : BaseViewModel
    {
        // Comandos para la navegación
        [RelayCommand]
        public async Task GoToHomePage()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new InicioPage());  // Navega a HomePage
        }

        [RelayCommand]
        public async Task GoToUsuariosPage()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new UsuarioPage(new UsuariosViewModel()));
        }

        [RelayCommand]
        public async Task GoToCarritoPage()
        {

            await Application.Current.MainPage.Navigation.PushAsync(new ProductoListaPage(new ProductoListaViewModel()));

            //await Shell.Current.GoToAsync(nameof(ProductoListaPage));
        }

        [RelayCommand]
        public async Task GoToPedidosPage()
        {
            //await Application.Current.MainPage.Navigation.PushAsync(new PedidosPage());  // Navega a PedidosPage
        }

        [RelayCommand]
        private async Task Volver()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ProductoListaPage(new ProductoListaViewModel()));

        }
    }
}
