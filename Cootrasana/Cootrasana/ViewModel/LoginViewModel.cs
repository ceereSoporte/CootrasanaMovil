

namespace Cootrasana.ViewModel
{
    using Cootrasana.Models;
    using Cootrasana.Services;
    using Cootrasana.Views;
    using GalaSoft.MvvmLight.Command;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;  

    public class LoginViewModel : BaseViewModel
    {
        #region Attributes

        private LoginDataBase loginModel;
        private LoginModel login;
        //private UsuariosModel User;
        //private UsuariosDataBase UserModel;
        private ApiService apiService;
        public bool isEnable;
        private bool isRunning;

        #endregion

        #region Properties

        public string Usuario { get; set; }
        public string Clave { get; set; }
        public List<LoginModel> MyLogin { get; set; }

        public bool IsEnable
        {
            get { return this.isEnable; }
            set { this.SetValue(ref this.isEnable, value); }
        }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { this.SetValue(ref this.isRunning, value); }
        }

        #endregion

        #region Constructor
        public LoginViewModel()
        {
            this.apiService = new ApiService();
            this.IsEnable = true;
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
            this.IsRunning = true;
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Aceptar");
                return;
            }

            this.IsEnable = false;
            var loginPOS = new LoginModel
            {
                user = this.Usuario,
                password = this.Clave
            };

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlPassController"].ToString();
            var response = await this.apiService.PostLogin<LoginModel>(url, prefix, controller, loginPOS);

            login = new LoginModel();
            loginModel = new LoginDataBase();

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("", "Usuario y/o contraseña incorrecta ", "Aceptar");
                this.IsEnable = true;
                this.IsRunning = false;
                return;
            }

            var usuario = (LoginModel)response.Result;

            //Guardado del usurio cuando existe
            login.id = usuario.id;
            login.user = usuario.user;
            login.password = usuario.password;
            login.nombres = usuario.nombres;
            login.apellidos = usuario.apellidos;
            loginModel.AddMember(login);

            MainViewModel.GetInstance().Viajes = new ViajesViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new ViajesPage());
            this.IsEnable = true;
            this.IsRunning = false;
        }       

        #endregion
    }
}
