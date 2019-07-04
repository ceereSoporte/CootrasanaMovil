

namespace Cootrasana.ViewModel
{
    using Cootrasana.Models;
    using Cootrasana.Views;
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LoginViewModel : BaseViewModel
    {
        #region Attributes

        public LoginDataBase crud;
        public LoginModel login;

        #endregion

        #region Properties

        public TicketsViewModel Tickets { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }

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
            login = new LoginModel();
            await Application.Current.MainPage.Navigation.PushAsync(new TicketsPage());
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
            //login = new LoginModel();
            ////crud = new LoginDataBase();
            ////login.Usuario = Usuario;
            ////login.Clave = Clave;
            ////crud.AddMember(login);
            //await Application.Current.MainPage.Navigation.PushAsync(new TicketsPage());
            
        }

        #endregion
    }
}
