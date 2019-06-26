

namespace Cootrasana.ViewModel
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;

    public class TicketsViewModel : BaseViewModel
    {
        #region Attributes
        public bool isEnable;
        #endregion

        #region Properties

        public int ValTicket { get; set; }

        public int NoPersonas { get; set; }

        public bool IsEnable
        {
            get { return this.isEnable; }
            set { this.SetValue(ref this.isEnable, value); }
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
            await App.Current.MainPage.DisplayAlert("Test", "Test", "OK");
        }
    }
}
