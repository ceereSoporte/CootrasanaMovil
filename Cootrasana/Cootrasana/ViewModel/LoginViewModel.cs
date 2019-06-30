

namespace Cootrasana.ViewModel
{
    using Cootrasana.Views;
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LoginViewModel : BaseViewModel
    {
        #region Attributes

        #endregion

        #region Properties

        public TicketsViewModel Tickets { get; set; }

        #endregion

        #region Constructor
        public LoginViewModel()
        {
            this.Tickets = new TicketsViewModel();
        }
        #endregion



        #region Command
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }


        private async void Login()
        {
            await App.Current.MainPage.DisplayAlert(
                "Logueo",
                "Acá se loguea",
                "OK");
        }

        public ICommand RegisterCommand
        {
            get
            {
                return new RelayCommand(Register);
            }
        }

        private async void Register()
        {
            
            await Application.Current.MainPage.Navigation.PushAsync(new TicketsPage());
        }

        #endregion
    }
}
