using Cootrasana.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cootrasana.ViewModel
{
    public class MainViewModel
    {
        public TicketsViewModel Tickets { get; set; }
        public LoginViewModel Login { get; set; }
        public ViajesViewModel Viajes { get; set; }
        public ValidarUsuariosViewModel Validar { get; set; }
        public LoginModel UserASP { get; set; }

        public MainViewModel()
        {
            instance = this;
        }

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
        #endregion
    }
}
