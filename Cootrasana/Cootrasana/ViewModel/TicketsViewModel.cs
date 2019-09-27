
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
        private LoginDataBase LoginModel;
        private LoginModel Login;
        private bool isEnableAct;
        private bool isVisible;
        private bool isVisibleAlert;
        private bool isVisibleOrigen;
        private bool isVisibleOrigenPick;
        private bool isToggled;
        private bool isEnableVal;
        private bool alerta;
        private bool isRunning;
        private bool tickPer;
        private bool tickEnco;
        private int noPersonas;
        private int valTicket;
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
        private int val;
        private readonly IBlueToothService blueToothService;
        private IList<string> deviceList;
        private string selectedDevice;
        private string mensaje;
        private string Bus;
        private string Placa;
        private int Posicion;
        private string valor;


        #endregion

        #region Properties

        public string Mensaje { get; set; }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { this.SetValue(ref this.isRunning, value); }
        }

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

        public int ValTicket
        {
            get { return this.valTicket; }
            set
            {
                this.SetValue(ref this.valTicket, value);
                this.Valor = String.Format("{0, 0:C0}", ValTicket);
            }
        }

        public string Valor
        {
            get { return this.valor; }
            set { this.SetValue(ref this.valor, value); }
        }

        public int NoPersonas
        {
            get { return this.noPersonas; }
            set { this.SetValue(ref this.noPersonas, value); }
        }

        public bool IsEnableAct
        {
            get { return this.isEnableAct; }
            set { this.SetValue(ref this.isEnableAct, value); }
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
        
        public bool IsVisible
        {
            get { return this.isVisible; }
            set { this.SetValue(ref this.isVisible, value); }
        }

        public bool TickPer
        {
            get { return this.tickPer; }
            set { this.SetValue(ref this.tickPer, value); }
        }

        public bool TickEnco
        {
            get { return this.tickEnco; }
            set { this.SetValue(ref this.tickEnco, value); }
        }

        #endregion

        #region Constructor

        public TicketsViewModel()
        {
            this.IsEnableAct = true;
            this.IsEnableVal = true;
            this.AlertaTicket = false;
            this.IsVisible = true;
            this.IsVisibleOrigen = false;
            this.IsVisibleOrigenPick = true;
            this.IsVisibleAlert = true;
            this.TickPer = true;
            this.TickEnco = false;
            this.NoPersonas = 1;
            this.ValTicket = val * NoPersonas;

            this.apiService = new ApiService();
            this.IntermediosModel = new IntermediosDataBase();
            this.UbicacionesModel = new UbicacionesDataBase();
            this.LoadIntermedios();
            blueToothService = DependencyService.Get<IBlueToothService>();
            this.OrigenPickSgte = null;
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
            this.IsRunning = true;
            this.IsEnableAct = false;

            var viaje = ViajeModel.GetMembers();

            if (selectedDevice == null)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debes de seleccionar una impresora", "Aceptar");
                this.IsRunning = false;
                this.IsEnableAct = true;
                return;
            }

            if (TickEnco == false)
            {
                if (Intermedios.destino == "" || Intermedios.origen == "" || ValTicket <= 0)
                {
                    await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debes llenar todos los campos",
                    "OK");
                    this.IsRunning = false;
                    this.IsEnableAct = true;
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
                        Mensaje = "             COOTRASANA" + "\n" + "  Cooperativa de Trasportadores" + "             San Antonio" + "\n\n" + "Ticket Persona(s) Alerta" + "\n\n" + "Fecha: " + DateTime.Now + "\n\n" + "Origen: " + Ubicaciones.nombre + "\n\n" + "Destino: " + Intermedios.destino + "\n\n" + "Bus: " + Bus + "\n\n" + "Placa: " + Placa + "\n\n" + "No Persona(s): " + NoPersonas + "\n\n" + "--------------------------------\n\n";
                    }
                    else
                    {
                        Mensaje = "             COOTRASANA" + "\n" + "  Cooperativa de Trasportadores" + "             San Antonio" + "\n\n" + "Ticket Persona(s)" + "\n\n" + "Fecha: " + DateTime.Now + "\n\n" + "Origen: " + Ubicaciones.nombre + "\n\n" + "Destino: " + Intermedios.destino + "\n\n" + "Bus: " + Bus + "\n\n" + "Placa: " + Placa + "\n\n" + "No Persona(s): " + NoPersonas + "\n\n" + "Valor: " + Valor + "\n\n" + "--------------------------------\n\n";
                    }
                    Imprimir(Mensaje);
                    Tickets.Origen = Ubicaciones.nombre;
                    Tickets.Destino = Intermedios.destino;
                    Tickets.ValTicket = ValTicket;
                    Tickets.NoPersonas = NoPersonas;
                    Tickets.Fecha = Fecha;
                    Tickets.Encomienda = false;
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
                    this.IsRunning = false;
                    this.IsEnableAct = true;
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
                    this.IsRunning = false;
                    this.IsEnableAct = true;
                }
                else
                {
                    foreach (var item in viaje)
                    {
                        Bus = item.Bus;
                        Placa = item.Placa;
                    }

                    Mensaje = "             COOTRASANA" + "\n" + "  Cooperativa de Trasportadores" + "             San Antonio" + "\n\n" + "Ticket Encomienda" + "\n\n" + "Fecha: " + DateTime.Now + "\n\n" + "Origen: " + Ubicaciones.nombre + "\n\n" + "Destino: " + Intermedios.destino + "\n\n" + "Bus: " + Bus + "\n\n" + "Placa: " + Placa + "\n\n" + "Valor: " + Valor + "\n\n" + "--------------------------------\n\n";
                    Imprimir(Mensaje);
                    Tickets.Origen = Ubicaciones.nombre;
                    Tickets.Destino = Intermedios.destino;
                    Tickets.ValTicket = ValTicket;
                    Tickets.Fecha = Fecha;
                    Tickets.NoPersonas = 0;
                    Tickets.Encomienda = true;
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
                    this.IsRunning = false;
                    this.IsEnableAct = true;
                }

            }
        }

        public void Imprimir(string Men)
        {
            blueToothService.Print(SelectedDevice, Men);
        }

        public void ClearControll()
        {
            NoPersonas = 1;
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

            this.IsRunning = true;
            this.IsEnableAct = false;

            TicketsModel = new TicketsDataBase();
            LoginModel = new LoginDataBase();

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Aceptar");
                this.IsRunning = false;
                this.IsEnableAct = true;
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
                    await App.Current.MainPage.DisplayAlert("El servicio esta malo", "Comunicate con el administrador", "Aceptar");
                    this.IsRunning = false;
                    this.IsEnableAct = true;
                    return;
                }
            }

            var consulta = TicketsModel.GetMembers();
            int PersonasInt = 0;
            int EncomiendasInt = 0;
            foreach (var item in consulta)
            {
                if (item.Encomienda)
                {
                    EncomiendasInt += item.ValTicket;
                }
                else
                {
                    PersonasInt += item.ValTicket;
                }
            }

            int TotalInt = PersonasInt + EncomiendasInt;
            string Total = string.Format("{0, 0:C0}", TotalInt);
            string Personas = string.Format("{0, 0:C0}", PersonasInt);
            string Encomiendas = string.Format("{0, 0:C0}", EncomiendasInt);

            Mensaje = "             COOTRASANA" + "\n" + "  Cooperativa de Trasportadores" + "             San Antonio" + "\n\n" + "Debes Liquidar" + "\n\n" + "Fecha: " + DateTime.Now  + "\n\n" + "Por Personas: " + Personas + "\n\n" + "Por Encomiendas: " + Encomiendas + "\n\n" + "Total: " + Total + "\n\n" + "Peajes: " + "\n\n" + "Combustible: " + "\n\n" + "Viaticos: " + "\n\n" +"Otros: " + "\n\n" + "Total: " + "\n\n" + "--------------------------------\n\n";
            Imprimir(Mensaje);
            await Application.Current.MainPage.DisplayAlert("Debes Liquidar", "Por personas: " +  Personas + "\n" + "Por Encomienda: " + Encomiendas + "\n" + "Total: " + Total, "Aceptar");
            Imprimir(Mensaje);
            TicketsModel.DeleteTable();
            LoginModel.DeleteTable();
            await Application.Current.MainPage.Navigation.PopAsync();
            MainViewModel.GetInstance().Login = new LoginViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
            this.IsRunning = false;
            this.IsEnableAct = true;
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
            if (Ubicaciones == null)
            {
                App.Current.MainPage.DisplayAlert("Error", "Debes selecciona un origen base", "Aceptar");
                return;
            }
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
            if (Ubicaciones == null)
            {
                App.Current.MainPage.DisplayAlert("Error", "Debes selecciona un origen base", "Aceptar");
                return;
            }

            UbicacionesModel = new UbicacionesDataBase();

            Posicion = Ubicaciones.posicion - 1;
            //var Sgte = OrigenPicket.Where(i => i.posicion == Posicion);
            OrigenPickSgte = new ObservableCollection<UbicacionesModel>(UbicacionesModel.GetMembers().Where(i => i.posicion == Posicion).OrderBy(i => i.nombre));
            if (OrigenPickSgte.Count == 0)
            {
                App.Current.MainPage.DisplayAlert("Error", "Este es el primer dato", "Aceptar");
                return;
            }

            IsVisibleOrigen = true;
            IsVisibleOrigenPick = false;
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

        public ICommand ValidarTicket
        {
            get
            {
                return new RelayCommand(Validar);
            }
        }

        private async void Validar()
        {
            MainViewModel.GetInstance().Validar = new ValidarUsuariosViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new ValidarUsuariosPage());
        }

        public ICommand Person
        {
            get
            {
                return new RelayCommand(TickPersona);
            }
        }

        private void TickPersona()
        {
            IsVisible = false;
            IsVisibleAlert = false;
            ValTicket = 0;
            NoPersonas = 0;
            IsEnableVal = true;
            AlertaTicket = false;
            TickPer = false;
            TickEnco = true;
        }

        public ICommand Encomienda
        {
            get
            {
                return new RelayCommand(TickEncomienda);
            }
        }

        private void TickEncomienda()
        {
            IsVisible = true;
            IsVisibleAlert = true;
            ValTicket = 0;
            NoPersonas = 1;
            IsEnableVal = false;
            TickPer = true;
            TickEnco = false;
        }

        #endregion

    }
}
