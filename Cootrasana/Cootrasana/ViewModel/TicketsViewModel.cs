

namespace Cootrasana.ViewModel
{
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class TicketsViewModel : BaseViewModel
    {
        #region Attributes
        public bool isEnable;
        public bool isVisible;
        public bool isToggled;
        public int noPersonas;
        public int valTicket;
        public string destino;
        public string origen;

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

        public int ValTicket
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
        //public string IsToggled
        //{
        //    get { return this.isToggled; }
        //    set { this.SetValue(ref this.isToggled, value); }
        //}

        public bool IsToggled
        {
            get { return this.isToggled; }
            set
            {
                isToggled = value;
                OnPropertyChanged(nameof(IsToggled2));
            }
        }

        public void IsToggled2()
        {
            if (IsToggled == true)
            {
               IsVisible = false;
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
            this.IsToggled = false;
            this.IsVisible = false;
            this.NoPersonas = 0;
            this.ValTicket = 0;
        }
        #endregion

        

        public ICommand PrintCommand
        {
            get
            {
                return new RelayCommand(Print);
            }
        }


        private async void Print()
        {
            await App.Current.MainPage.DisplayAlert(
                "Imprimir",
                "Origen: " + Origen + "\n" + "Destino: " + Destino + "\n" + "Número de personas: " + NoPersonas  + "\n" + "Valor de Tickets: " + ValTicket,
                "OK");
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
            await App.Current.MainPage.DisplayAlert("Alerta", "Acá va la alerta del ticket", "OK");
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

    }
}
