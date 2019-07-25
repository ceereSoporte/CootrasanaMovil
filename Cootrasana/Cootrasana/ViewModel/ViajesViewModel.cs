using Cootrasana.Models;
using Cootrasana.Services;
using Cootrasana.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Cootrasana.ViewModel
{
    public class ViajesViewModel : BaseViewModel
    {
        #region Attributes

        private ApiService apiService;
        private ViajesDataBase viajesModel;    
        private LoginDataBase LoginModel;
        private LoginModel login;
        private IntermediosModel intermedios;
        private IntermediosDataBase intermediosModel;
        private ObservableCollection<ViajesModel> viajespick;
        private ViajesModel viajes;
        private int idusuario;
        private bool isEnable;
        #endregion

        #region properties
        public int ID { get; set; }

        public ViajesModel Viajes { get; set; }

        public string Nombre { get; set; }

        public bool IsEnable
        {
            get { return this.isEnable; }
            set { this.SetValue(ref this.isEnable, value); }
        }

        public ObservableCollection<ViajesModel> Viajespick
        {
            get { return this.viajespick; }
            set { this.SetValue(ref this.viajespick, value); }
        }

        public List<GeneralModel> MyViajes { get; set; }
        public List<ViajesModel> MyIntermedios { get; set; }

        #endregion

        #region Constructor

        public ViajesViewModel()
        {
            this.apiService = new ApiService();
            this.IsEnable = true;
            this.LoadViaje();
        }
        #endregion

        #region Command
        public async void LoadViaje()
        {
            LoginModel = new LoginDataBase();
            viajesModel = new ViajesDataBase();
            viajes = new ViajesModel();
            intermedios = new IntermediosModel();
            intermediosModel = new IntermediosDataBase();



            var usua = LoginModel.GetMembers();
            foreach (var item in usua)
            {
                this.idusuario = item.id;
            }
            var loginPOS = new GeneralModel
            {
                id = idusuario,
            };

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlViajes"].ToString();
            var response = await this.apiService.Post<GeneralModel>(url, prefix, controller, loginPOS);

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error","Este usuario aun no tiene viajes asignados","Aceptar");
                return;
            }

            this.MyViajes = (List<GeneralModel>)response.Result;

            if (MyViajes != null)
            {
                viajesModel.DeleteTable();
            }

            //Agregar a la tabla de viajes
            foreach (var item in MyViajes)
            {
                viajes.id = item.id;
                viajes.nombre = item.nombre + " " + item.horaViaje;
                viajes.idOrigen = item.idOrigen;
                viajes.idDestino = item.idDestino;
                viajes.origen = item.origen;
                viajes.destino = item.destino;
                viajes.valor = item.valor;
                viajesModel.AddMember(viajes);
            }

            this.Viajespick = new ObservableCollection<ViajesModel>(viajesModel.GetMembers().OrderBy(x => x.nombre));
        }

        public ICommand EnviarCommand
        {
            get
            {
                return new RelayCommand(LoadIntermedio);
            }
        }
        
        private async void LoadIntermedio()
        {
            this.IsEnable = false;

            if (Viajespick == null)
            {
                await App.Current.MainPage.DisplayAlert("Error","debes selecionar un viaje","Aceptar");
                return;
            }

            var Load = new ViajesModel
            {
                id = this.Viajes.id,
            };

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlIntermedios"].ToString();
            var response = await this.apiService.Post<ViajesModel>(url, prefix, controller, Load);

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Esta ruta no posee intermedios, comuniquese con el administrador", "Aceptar");
                return;
            }

            this.MyIntermedios = (List<ViajesModel>)response.Result;

            var consul = viajesModel.GetOneMembers(this.Viajes.id);

            if (MyIntermedios != null)
            {
                intermediosModel.DeleteTable();
            }

            // se agrega el viaje que tambien se convierte en un intermedio
            foreach (var item in consul)
            {
                intermedios.id = item.id;
                intermedios.idOrigen = item.idOrigen;
                intermedios.idDestino = item.idDestino;
                intermedios.origen = item.origen;
                intermedios.destino = item.destino;
                intermedios.valor = item.valor;
                intermediosModel.AddMember(intermedios);
            }


            foreach (var item in MyIntermedios)
            {
                //Agregar a la tabla de los intermedios
                intermedios.id = item.id;
                intermedios.idOrigen = item.idOrigen;
                intermedios.idDestino = item.idDestino;
                intermedios.origen = item.origen;
                intermedios.destino = item.destino;
                intermedios.valor = item.valor;
                intermediosModel.AddMember(intermedios);
            }

            MainViewModel.GetInstance().Tickets = new TicketsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new TicketsPage());
            this.IsEnable = true;
        }

        #endregion
    }
}
