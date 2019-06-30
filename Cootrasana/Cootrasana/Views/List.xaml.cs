using Cootrasana.Models;


namespace Cootrasana.Views
{
    using System;
    using Xamarin.Forms;

    public partial class List : ContentPage
	{
        public TicketsDataBase Tickets;

        public List ()
		{
			InitializeComponent ();

            Tickets = new TicketsDataBase();
            var tickets = Tickets.GetMembers();
            listMembers.ItemsSource = tickets;
        }

        public async void OnSelected(object obj, ItemTappedEventArgs args)
        {
            var tickets = args.Item as TicketsModel;
            await DisplayAlert(
                    "Imprimir",
                    "Origen: " + tickets.Origen + "\n" + "Destino: " + tickets.Destino + "\n" + "Número de personas: " + tickets.NoPersonas + "\n" + "Valor de Tickets: $" + tickets.ValTicket + "\n" + "Fecha: " + tickets.Fecha,
                    "OK");
        }
    }
}