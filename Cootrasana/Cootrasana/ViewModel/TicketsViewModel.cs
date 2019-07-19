

namespace Cootrasana.ViewModel
{
    using Cootrasana.Models;
    using Cootrasana.Services;
    using Cootrasana.Views;
    using GalaSoft.MvvmLight.Command;
    using System;
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
        private int noPersonas;
        private Double valTicket;
        private string destino;
        private string origen;
        private ApiService apiService;


        #endregion

        #region Properties

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
            this.IsVisible = true;
            this.IsVisibleAlert = true;
            this.NoPersonas = 0;
            this.ValTicket = 0;
            this.apiService = new ApiService();
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
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Aceptar");
                return;
            }



            if (IsToggled == false)
            {
                if (Destino == "" || Origen == "" || NoPersonas <= 0)
                {
                    await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debes llenar todos los campos",
                    "OK");
                }
                else
                {
                    Tickets = new TicketsModel();
                    TicketsModel = new TicketsDataBase();
                    Tickets.Origen = Origen;
                    Tickets.Destino = Destino;
                    Tickets.ValTicket = ValTicket;
                    Tickets.NoPersonas = NoPersonas;
                    Tickets.Fecha = Fecha;
                    Tickets.Encomienda = isToggled;
                    TicketsModel.AddMember(Tickets);
                    ClearControll();
                    //await App.Current.MainPage.DisplayAlert(
                    //"Imprimir",
                    //"Origen: " + Origen + "\n" + "Destino: " + Destino + "\n" + "Número de personas: " + NoPersonas + "\n" + "Valor de Tickets: $" + ValTicket + "\n" + "Fecha: " + DateTime.Now,
                    //"OK");
                }
            }

            else
            {
                if (Destino == "" || Origen == "" || ValTicket <= 0)
                {
                    await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debes llenar todos los campos",
                    "OK");
                }
                else
                {
                    Tickets = new TicketsModel();
                    TicketsModel = new TicketsDataBase();
                    Tickets.Origen = Origen;
                    Tickets.Destino = Destino;
                    Tickets.ValTicket = ValTicket;
                    Tickets.Fecha = Fecha;
                    Tickets.Encomienda = isToggled;
                    TicketsModel.AddMember(Tickets);
                    ClearControll();
                    //await App.Current.MainPage.DisplayAlert(
                    //"Imprimir",
                    //"Origen: " + Origen + "\n" + "Destino: " + Destino + "\n" + "Valor encomienda: $" + ValTicket + "\n" + "Fecha: " + DateTime.Now,
                    //"OK");
                }

            }
        }

        public void ClearControll()
        {
            NoPersonas = 0;
            Origen = "";
            Destino = "";
            ValTicket = 0;
        }

        public ICommand ClearCampos
        {
            get
            {
                return new RelayCommand(EliminarDatos);
            }
        }

        private void EliminarDatos()
        {
            TicketsModel = new TicketsDataBase();
            TicketsModel.DeleteTable();
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
                ValTicket = NoPersonas * 14000;
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
           ValTicket = NoPersonas * 14000;
        }

        #endregion

    }
}
