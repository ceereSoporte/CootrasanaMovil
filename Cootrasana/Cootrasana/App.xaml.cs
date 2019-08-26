using Cootrasana.Models;
using Cootrasana.ViewModel;
using Cootrasana.Views;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Cootrasana
{
    public partial class App : Application
    {
        public static NavigationPage Navigator { get; internal set; }

        public App()
        {
            InitializeComponent();

            LoginDataBase LoginModel;
            LoginModel login;
            var mainViewModel = MainViewModel.GetInstance();
            login = new LoginModel();
            LoginModel = new LoginDataBase();

            var usu =  LoginModel.GetMembers();
            
            foreach (var item in usu)
            {
                mainViewModel.Tickets = new TicketsViewModel();
                this.MainPage = new NavigationPage(new TicketsPage());
                return;
            }
            mainViewModel.Login = new LoginViewModel();
            this.MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
