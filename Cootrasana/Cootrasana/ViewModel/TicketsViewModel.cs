﻿
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
        private bool isVisibleOrigen;
        private bool isVisibleOrigenPick;
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
        private ObservableCollection<UbicacionesModel> origenPickSgte;
        private IntermediosDataBase IntermediosModel;
        private UbicacionesDataBase UbicacionesModel;
        private UbicacionesModel ubicaciones;
        private IntermediosModel intermedios;
        private double val;
        private readonly IBlueToothService blueToothService;
        private IList<string> deviceList;
        private string selectedDevice;
        private string mensaje;
        private string Bus;
        private string Placa;
        private int Posicion;


        #endregion

        #region Properties

        public string Mensaje { get; set; }

        public IList<string> DeviceList
        {
            get
            {
                if (deviceList == null)
                    deviceList = new ObservableCollection<string>();
                return deviceList;
            }
            set
            {
                deviceList = value;
            }
        }

        public string SelectedDevice
        {
            get
            {
                return selectedDevice;
            }
            set
            {
                selectedDevice = value;
            }
        }

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
                if (ubicaciones != null)
                {
                    this.DestinoPick = new ObservableCollection<IntermediosModel>(IntermediosModel.GetOneMembers(Ubicaciones.id).OrderBy(i => i.destino));
                }
            }
        }


        public List<IntermediosModel> MyIntermedios { get; set; }
        public List<TicketsModel> consulta { get; set; }

        public ObservableCollection<IntermediosModel> DestinoPick
        {
            get { return this.destinoPick; }
            set { this.SetValue(ref this.destinoPick, value); }
        }

        public ObservableCollection<UbicacionesModel> OrigenPickSgte
        {
            get { return this.origenPickSgte; }
            set { this.SetValue(ref this.origenPickSgte, value); }
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

        public bool IsVisibleOrigen
        {
            get { return this.isVisibleOrigen; }
            set { this.SetValue(ref this.isVisibleOrigen, value); }
        }

        public bool IsVisibleOrigenPick
        {
            get { return this.isVisibleOrigenPick; }
            set { this.SetValue(ref this.isVisibleOrigenPick, value); }
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
                    AlertaTicket = false;
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
            this.IsVisibleOrigen = false;
            this.IsVisibleOrigenPick = true;
            this.IsVisibleAlert = true;
            this.NoPersonas = 1;
            this.ValTicket = val * NoPersonas;
            this.apiService = new ApiService();
            this.IntermediosModel = new IntermediosDataBase();
            this.UbicacionesModel = new UbicacionesDataBase();
            this.LoadIntermedios();
            blueToothService = DependencyService.Get<IBlueToothService>();
            this.Posicion = 1;
            this.BindDeviceList();

        }

        private void LoadIntermedios()
        {
            this.OrigenPicket = new ObservableCollection<UbicacionesModel>(UbicacionesModel.GetMembers().OrderBy(i => i.posicion));
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

            if (selectedDevice == null)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debes de seleccionar una impresora", "Aceptar");
                return;
            }

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
                    foreach (var item in viaje)
                    {
                        Bus = item.Bus;
                        Placa = item.Placa;
                    }

                    if (AlertaTicket)
                    {
                        Mensaje = "             COOTRASANA" + "\n" + "  Cooperativa de Trasportadores" + "             San Antonio" + "\n\n" + "Ticket Personas" + "\n\n" + "Fecha: " + DateTime.Now + "\n\n" + "Origen: " + Ubicaciones.nombre + "\n\n" + "Destino: " + Intermedios.destino + "Bus: " + Bus + "\n\n" + "Placa: " + Placa + "\n\n" + "\n\n" + "NoPersona: " + NoPersonas + "\n\n" + "";
                    }
                    else
                    {
                        Mensaje = "             COOTRASANA" + "\n" + "  Cooperativa de Trasportadores" + "             San Antonio" + "\n\n" + "Ticket Personas" + "\n\n" + "Fecha: " + DateTime.Now + "\n\n" + "Origen: " + Ubicaciones.nombre + "\n\n" + "Destino: " + Intermedios.destino + "\n\n" + "Bus: " + Bus + "\n\n" + "Placa: " + Placa + "\n\n" + "NoPersona: " + NoPersonas + "\n\n" + "Valor: $" + ValTicket + "\n\n" + "";
                    }
                    Imprimir(Mensaje);
                    Tickets.Origen = Ubicaciones.nombre;
                    Tickets.Destino = Intermedios.destino;
                    Tickets.ValTicket = 0;
                    Tickets.NoPersonas = NoPersonas;
                    Tickets.Fecha = Fecha;
                    Tickets.Encomienda = isToggled;
                    Tickets.idDestino = Intermedios.idDestino;
                    Tickets.idOrigen = Ubicaciones.id;
                    foreach (var item in viaje)
                    {
                        Tickets.Hora = item.Hora;
                        Tickets.idViaje = item.id;
                        Tickets.Bus = item.Bus;
                        Tickets.Placa = item.Placa;
                    }
                    Tickets.Alert = AlertaTicket;
                    TicketsModel.AddMember(Tickets);
                    ClearControll();
                    this.AlertaTicket = false;
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
                    Mensaje = "             COOTRASANA" + "\n" + "  Cooperativa de Trasportadores" + "             San Antonio" + "\n\n" + "Ticket Encomienda" + "\n\n" + "Fecha: " + DateTime.Now + "\n\n" + "Origen: " + Ubicaciones.nombre + "\n\n" + "Destino: " + Intermedios.destino + "\n\n" + "Bus: " + Bus + "\n\n" + "Placa: " + Placa + "\n\n" + "Valor: $" + ValTicket + "\n\n" + "";
                    Imprimir(Mensaje);
                    Tickets.Origen = Ubicaciones.nombre;
                    Tickets.Destino = Intermedios.destino;
                    Tickets.ValTicket = 0;
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
                    this.AlertaTicket = false;
                }

            }
        }

        public void Imprimir(string Men)
        {
            blueToothService.Print(SelectedDevice, Men);
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
            if (AlertaTicket)
            {
                App.Current.MainPage.DisplayAlert("Alerta", "Este ticket ya posee una alerta", "Acepatr");
            }
            this.AlertaTicket = true;
            App.Current.MainPage.DisplayAlert("Alerta", "Has creado una alerta al ticket", "Acepatr");
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

            var answer = await Application.Current.MainPage.DisplayAlert("Confirmación", "¿Desea terminar el viaje?", "Sí", "No");
            if (!answer)
            {
                return;
            }


            TicketsModel = new TicketsDataBase();

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
                    await App.Current.MainPage.DisplayAlert("El servicio esta malo", "", "Aceptar");
                }
            }

            var consulta = TicketsModel.GetMembers();
            Decimal Personas = 0;
            Decimal Encomiendas = 0;
            foreach (var item in consulta)
            {
                if (item.Encomienda)
                {
                    Encomiendas += Convert.ToDecimal(item.ValTicket);
                }
                else
                {
                    Personas += Convert.ToDecimal(item.ValTicket);
                }
            }

            Decimal Total = Personas + Encomiendas;
            Mensaje = "             COOTRASANA" + "\n" + "  Cooperativa de Trasportadores" + "             San Antonio" + "\n\n" + "Debes Liquidar" + "\n\n" + "Por Personas: $" + Personas + "\n\n" + "Por Encomiendas: $" + Encomiendas + "\n\n" + "Total: $" + Total + "\n\n" + "";
            Imprimir(Mensaje);
            await Application.Current.MainPage.DisplayAlert("Debes Liquidar", "Por personas: " + Personas + "\n" + "Por Encomienda: " + Encomiendas + "\n" + "Total: " + Total, "Aceptar");

            TicketsModel.DeleteTable();
            MainViewModel.GetInstance().Login = new LoginViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }

        private void BindDeviceList()
        {
            var list = blueToothService.GetDeviceList();
            DeviceList.Clear();
            foreach (var item in list)
                DeviceList.Add(item);
        }

        public ICommand Siguiente
        {
            get
            {
                return new RelayCommand(SiguienteOrigen);
            }
        }

        private void SiguienteOrigen()
        {

            UbicacionesModel = new UbicacionesDataBase();
            Posicion = Ubicaciones.posicion + 1;
            //var Sgte = OrigenPicket.Where(i => i.posicion == Posicion);
            OrigenPickSgte = new ObservableCollection<UbicacionesModel>(UbicacionesModel.GetMembers().Where(i => i.posicion == Posicion).OrderBy(i => i.nombre));
            if (OrigenPickSgte.Count == 0)
            {
                App.Current.MainPage.DisplayAlert("Error", "Este es el último origen", "Aceptar");
                return;
            }
            
            foreach (var item in OrigenPickSgte)
            {
                IsVisibleOrigen = true;
                IsVisibleOrigenPick = false;
                Ubicaciones.id = item.id;
                ubicaciones.nombre = item.nombre;
                ubicaciones.posicion = Posicion;
                Origen = item.nombre;
                Posicion = item.posicion;
                this.DestinoPick = new ObservableCollection<IntermediosModel>(IntermediosModel.GetOneMembers(item.id).OrderBy(i => i.destino));
            }            
        }

        public ICommand Anterior
        {
            get
            {
                return new RelayCommand(AnteriorOrigen);
            }
        }

        private void AnteriorOrigen()
        {
            IsVisibleOrigen = true;
            IsVisibleOrigenPick = false;
            UbicacionesModel = new UbicacionesDataBase();
            Posicion = Ubicaciones.posicion - 1;
            //var Sgte = OrigenPicket.Where(i => i.posicion == Posicion);
            OrigenPickSgte = new ObservableCollection<UbicacionesModel>(UbicacionesModel.GetMembers().Where(i => i.posicion == Posicion).OrderBy(i => i.nombre));
            if (OrigenPickSgte.Count == 0)
            {
                App.Current.MainPage.DisplayAlert("Error", "Este es el primer dato", "Aceptar");
                return;
            }

            foreach (var item in OrigenPickSgte)
            {
                Ubicaciones.id = item.id;
                ubicaciones.nombre = item.nombre;
                ubicaciones.posicion = Posicion;
                Origen = item.nombre;
                Posicion = item.posicion;
                this.DestinoPick = new ObservableCollection<IntermediosModel>(IntermediosModel.GetOneMembers(item.id).OrderBy(i => i.destino));
            }
            
        }


        #endregion

    }
}
