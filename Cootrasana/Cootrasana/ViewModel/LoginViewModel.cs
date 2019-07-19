

namespace Cootrasana.ViewModel
{
    using Cootrasana.Models;
    using Cootrasana.Services;
    using Cootrasana.Views;
    using GalaSoft.MvvmLight.Command;
    using System.Collections.Generic;
    using System.Windows.Input;
    using Xamarin.Forms;  

    public class LoginViewModel : BaseViewModel
    {
        #region Attributes

        private LoginDataBase loginModel;
        private LoginModel login;
        private ApiService apiService;

        #endregion

        #region Properties

        public TicketsViewModel Tickets { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public List<LoginModel> MyLogin { get; set; }

        #endregion

        #region Constructor
        public LoginViewModel()
        {
            this.Tickets = new TicketsViewModel();
            this.apiService = new ApiService();
        }
        #endregion

        #region Singleton

        private static TicketsViewModel instance;

        public static TicketsViewModel GetInstance()
        {
            return instance;
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
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlUserController"].ToString();
            var response = await this.apiService.GetList<LoginModel>(url, prefix, controller);

            MyLogin = (List<LoginModel>)response.Result;

            login = new LoginModel();
            loginModel = new LoginDataBase();
            

            if (MyLogin.Count != 0)
            {
                loginModel.DeleteTable();
            }
            foreach (var item in MyLogin)
            {
                login.name = item.name;
                login.Password = item.Password;
                login.Nombres = item.Nombres;
                login.Apellidos = item.Apellidos;
                loginModel.AddMember(login);
            }

            bool usu = VerifyPassword();


            if (!usu)
            {
                await App.Current.MainPage.DisplayAlert("", "Usuario y/o contraseña incorrecta ", "Aceptar");
                return;
            }
            MainViewModel.GetInstance().Tickets = new TicketsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new TicketsPage());

        }

        private bool VerifyPassword()
        {
            var loginModel = new LoginModel
            {
                name = this.Usuario,
                Password = this.Clave
            };

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlPassController"].ToString();
            var response = this.apiService.Post<LoginModel>(url, prefix, controller,loginModel);

            if (!response.Result.IsSuccess)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        

        #endregion
    }
}
