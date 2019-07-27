

namespace Cootrasana.ViewModel
{
    using Cootrasana.Models;
    using Cootrasana.Services;
    using Cootrasana.Views;
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class TicketsViewModel : BaseViewModel
    {
        #region Attributes

        private TicketsDataBase TicketsModel;
        private TicketsModel Tickets;
        private bool isEnable;
        private bool isVisible;
        private bool isVisibleAlert;
        private bool isToggled;
        private bool isEnableVal;
        private bool alerta;
        private int noPersonas;
        private Double valTicket;
        private string destino;
        private string origen;
        private ApiService apiService;
        private ObservableCollection<IntermediosModel> destinoPick;
        private ObservableCollection<UbicacionesModel> origenPick;
        private IntermediosDataBase IntermediosModel;
        private UbicacionesDataBase UbicacionesModel;
        private UbicacionesModel ubicaciones;
        private IntermediosModel intermedios;
        private double val;


        #endregion

        #region Properties

        public IntermediosModel Intermedios
        {
            get { return this.intermedios; }
            set
            {
                this.SetValue(ref this.intermedios, value);
                
                if (Intermedios == null)
                {
                    return;
                }
                var Valor = IntermediosModel.GetVal(Ubicaciones.id, Intermedios.idDestino);

                foreach (var item in Valor)
                {
                    val = item.valor;
                }
                ValTicket = val * NoPersonas;
            }

        }

        public UbicacionesModel Ubicaciones
        {
            get { return this.ubicaciones; }
            set
            {
                this.SetValue(ref this.ubicaciones, value);
                this.DestinoPick = new ObservableCollection<IntermediosModel>(IntermediosModel.GetOneMembers(Ubicaciones.id).OrderBy(i => i.destino));
            }
        }
        

        public List<IntermediosModel> MyIntermedios { get; set; }

        public ObservableCollection<IntermediosModel> DestinoPick
        {
            get { return this.destinoPick; }
            set { this.SetValue(ref this.destinoPick, value); }
        }

        public ObservableCollection<UbicacionesModel> OrigenPicket
        {
            get { return this.origenPick; }
            set { this.SetValue(ref this.origenPick, value); }
        }

        public string Destino
        {
            get { return this.destino; }
            set { this.SetValue(ref this.destino, value); }
        }

        public string Origen
        {
            get { return this.origen; }
            set { this.SetValue(ref this.origen, value); }
        }

        public DateTime Fecha
        {
            get
            {
                return DateTime.Now;
            }
        }

        public Double ValTicket
        {
            get { return this.valTicket; }
            set { this.SetValue(ref this.valTicket, value); }
        }

        public int NoPersonas
        {
            get { return this.noPersonas; }
            set { this.SetValue(ref this.noPersonas, value); }
        }

        public bool IsEnable
        {
            get { return this.isEnable; }
            set { this.SetValue(ref this.isEnable, value); }
        }

        public bool IsEnableVal
        {
            get { return this.isEnableVal; }
            set { this.SetValue(ref this.isEnableVal, value); }
        }

        public bool IsVisibleAlert
        {
            get { return this.isVisibleAlert; }
            set { this.SetValue(ref this.isVisibleAlert, value); }
        }

        public bool AlertaTicket
        {
            get { return this.alerta; }
            set { this.SetValue(ref this.alerta, value); }
        }

        public bool IsToggled
        {
            get { return this.isToggled; }

            set
            {
                isToggled = value;
                if (isToggled == true)
                {
                    IsVisible = false;
                    IsVisibleAlert = false;
                    this.ValTicket = 0;
                    NoPersonas = 0;
                    IsEnableVal = true;

                }
                else if (isToggled == false)
                {
                    IsVisible = true;
                    IsVisibleAlert = true;
                    ValTicket = 0;
                    NoPersonas = 0;
                    IsEnableVal = false; 

                }
                OnPropertyChanged(nameof(IsToggled));
            }
        }

        public bool IsVisible
        {
            get { return this.isVisible; }
            set { this.SetValue(ref this.isVisible, value); }
        }

        #endregion

        #region Constructor

        public TicketsViewModel()
        {
            this.IsEnable = true;
            this.IsEnableVal = true;
            this.IsToggled = false;
            this.AlertaTicket = false;
            this.IsVisible = true;
            this.IsVisibleAlert = true;
            this.NoPersonas = 1;
            this.ValTicket = val * NoPersonas;
            this.apiService = new ApiService();
            this.IntermediosModel = new IntermediosDataBase();
            this.UbicacionesModel = new UbicacionesDataBase();
            this.LoadIntermedios();
        }

        private void LoadIntermedios()
        {
            this.OrigenPicket = new ObservableCollection<UbicacionesModel>(UbicacionesModel.GetMembers().OrderBy(i => i.nombre));
        }
        #endregion

        #region Command

        public ICommand PrintCommand
        {
            get
            {
                return new RelayCommand(Print);
            }
        }


        private async void Print()
        {
            Tickets = new TicketsModel();
            TicketsModel = new TicketsDataBase();
            
            var ViajeModel = new ViajesDataBase();

            var viaje = ViajeModel.GetMembers();

            if (IsToggled == false)
            {
                if (Intermedios.destino == "" || Intermedios.origen == "" || NoPersonas <= 0)
                {
                    await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debes llenar todos los campos",
                    "OK");
                }
                else
                {

                    Tickets.Origen = Ubicaciones.nombre;
                    Tickets.Destino = Intermedios.destino;
                    Tickets.ValTicket = ValTicket;
                    Tickets.NoPersonas = NoPersonas;
                    Tickets.Fecha = Fecha;
                    Tickets.Encomienda = isToggled;
                    Tickets.idDestino = Intermedios.idDestino;
                    Tickets.idOrigen = Ubicaciones.id;
                    foreach (var item in viaje)
                    {
                        Tickets.Hora = item.Hora;
                        Tickets.idViaje = item.id;
                    }
                    Tickets.Alert = AlertaTicket;
                    TicketsModel.AddMember(Tickets);
                    ClearControll();
                }
            }

            else
            {
                if (Intermedios.destino == "" || Intermedios.origen == "" || ValTicket <= 0)
                {
                    await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debes llenar todos los campos",
                    "OK");
                }
                else
                {
                    Tickets.Origen = Ubicaciones.nombre;
                    Tickets.Destino = Intermedios.destino;
                    Tickets.ValTicket = ValTicket;
                    Tickets.Fecha = Fecha;
                    Tickets.NoPersonas = 0;
                    Tickets.Encomienda = isToggled;
                    Tickets.idDestino = Intermedios.idDestino;
                    Tickets.idOrigen = Ubicaciones.id;
                    foreach (var item in viaje)
                    {
                        Tickets.Hora = item.Hora;
                        Tickets.idViaje = item.id;
                    }
                    Tickets.Alert = AlertaTicket;
                    TicketsModel.AddMember(Tickets);
                    ClearControll();
                }

            }
        }

        public void ClearControll()
        {
            NoPersonas = 0;
            Intermedios.destino = "";
            ValTicket = 0;
            AlertaTicket = false;
        }
        
        public ICommand AlertCommand
        {
            get
            {
                return new RelayCommand(Alert);
            }
        }

        private async void Alert()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new List());
            //await App.Current.MainPage.DisplayAlert("Alerta", "Acá va la alerta del ticket", "OK");
        }

        public ICommand RigthCommand
        {
            get
            {
                return new RelayCommand(Rigth);
            }
        }

        private async void Rigth()
        {
            if ((NoPersonas - 1) < 0)
            {
                await App.Current.MainPage.DisplayAlert("Alerta", "El número de personas no puede ser negativo", "OK");
            }
            else
            {
                NoPersonas = NoPersonas - 1;
                ValTicket = NoPersonas * val;
            }            
        }

        public ICommand LeftCommand
        {
            get
            {
                return new RelayCommand(Left);
            }
        }

        private void Left()
        {
           NoPersonas = NoPersonas + 1;
           ValTicket = NoPersonas * val;
        }

        public ICommand Alerta
        {
            get
            {
                return new RelayCommand(Alarma);
            }
        }

        private void Alarma()
        {
            this.AlertaTicket = true;
        }

        public ICommand FinishCommand
        {
            get
            {
                return new RelayCommand(Finish);
            }
        }

        private async void Finish()
        {
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Aceptar");
                return;
            }
            
            var POS = TicketsModel.GetMembers();

            foreach (var item in POS)
            {
                var TicketsPOS = new TicketsModel
                {
                    Origen = item.Origen,
                    Destino = item.Destino,
                    idOrigen = item.idOrigen,
                    idDestino = item.idDestino,
                    NoPersonas = item.NoPersonas,
                    ValTicket = item.ValTicket,
                    Encomienda = item.Encomienda,
                    Alert = item.Alert,
                    Hora = item.Hora,
                    idViaje = item.idViaje,
                    Fecha = item.Fecha
                };

                var url = Application.Current.Resources["UrlAPI"].ToString();
                var prefix = Application.Current.Resources["UrlPrefix"].ToString();
                var controller = Application.Current.Resources["UrlTicket"].ToString();
                var response = await this.apiService.PostPrint<TicketsModel>(url, prefix, controller, TicketsPOS);

                if (!response.IsSuccess)
                {
                    await App.Current.MainPage.DisplayAlert("El servicio esta malo","","Aceptar");
                }
            }

            TicketsModel.DeleteTable();
            MainViewModel.GetInstance().Login = new LoginViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }

        #endregion

    }
}
