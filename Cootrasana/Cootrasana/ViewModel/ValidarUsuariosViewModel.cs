using Cootrasana.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Cootrasana.ViewModel
{
    public class ValidarUsuariosViewModel : BaseViewModel
    {
        #region Attributes
        private int noTicket;
        private UsuariosViajeModel UsuariosViaje;
        private UsuariosViajeDataBase UsuariosViajeModel;
        #endregion

        #region Properties
        public int NoTicket
        {
            get { return this.noTicket; }
            set { this.SetValue(ref this.noTicket, value); }
        }
        #endregion

        #region Constructor
        public ValidarUsuariosViewModel()
        {
            this.NoTicket = 0;
        }
        #endregion

        #region Common

        public ICommand EnviarCommand
        {
            get { return new RelayCommand(Validar); }
        }

        private async void Validar()
        {
            this.UsuariosViaje = new UsuariosViajeModel();
            this.UsuariosViajeModel = new UsuariosViajeDataBase();

            if (NoTicket == 0)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debes digitar el numero de ticket", "Aceptar");
            }

            List<UsuariosViajeModel> validar = (List<UsuariosViajeModel>)UsuariosViajeModel.GetOneMembers(NoTicket);

            if (validar.Count != 0)
            {
                foreach (var item in validar)
                {
                    var answer = await App.Current.MainPage.DisplayAlert("Ticket", "Documento: " + item.documento + "\n" + "Nombre: " + item.nombres + "\n" + "Apellido: " + item.apellidos + "\n" + "Puesto: " + item.Puesto, "Aceptar","Digito otro");
                    if (answer)
                    {
                       await Application.Current.MainPage.Navigation.PopAsync();
                    }
                }

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Este ticket no existe", "Aceptar");
            }
        }

        #endregion

    }
}
