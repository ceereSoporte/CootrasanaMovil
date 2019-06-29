

namespace Cootrasana.ViewModel
{
    using Cootrasana.Views;
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;

    public class LoginViewModel : BaseViewModel
    {
        #region Attributes

        #endregion

        #region Constructor
        public LoginViewModel()
        {

        }
        #endregion

        #region Command

        private ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }

        public async void Login()
        {
            await App.Current.MainPage.DisplayAlert(
                "Logueo",
                "Acá se loguea",
                "OK");
        }

        private ICommand RegisterCommand
        {
            get
            {
                return new RelayCommand(Register);
            }
        }

        private async void Register()
        {
            await App.Current.MainPage.DisplayAlert("Registro","Acá va el registro","OK");

            await App.Current.MainPage.Navigation.PushAsync(new TicketsPage());
        }

        #endregion
    }
}
