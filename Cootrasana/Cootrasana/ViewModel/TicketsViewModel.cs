using System.Windows.Input;

namespace Cootrasana.ViewModel
{

    public class TicketsViewModel : BaseViewModel
    {

        public int ValTicket { get; set; }

        public int NoPersonas { get; set; }



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
