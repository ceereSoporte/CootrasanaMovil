using Cootrasana.Models;


namespace Cootrasana.Views
{
    using System;
    using Xamarin.Forms;

    public partial class List : ContentPage
	{
        public LoginDataBase Tickets;

        public List ()
		{
			InitializeComponent ();

            Tickets = new LoginDataBase();
            var tickets = Tickets.GetMembers();
            listMembers.ItemsSource = tickets;
        }

        public async void OnSelected(object obj, ItemTappedEventArgs args)
        {
            var tickets = args.Item as LoginModel;

            await DisplayAlert(
             "Persona",
             "Origen: " + tickets.name + "\n" + "Destino: " + tickets.Password + "\n" + "Número de personas: " + tickets.Nombres + "\n" + "Valor de Tickets: $" + tickets.Apellidos ,
             "OK");

            //if (tickets.Encomienda == false)
            //{
            //    await DisplayAlert(
            //     "Persona",
            //     "Origen: " + tickets.Origen + "\n" + "Destino: " + tickets.Destino + "\n" + "Número de personas: " + tickets.NoPersonas + "\n" + "Valor de Tickets: $" + tickets.ValTicket + "\n" + "Fecha: " + tickets.Fecha,
            //     "OK");
            //}
            //else
            //{
            //    await DisplayAlert(
            //     "Encomienda",
            //     "Origen: " + tickets.Origen + "\n" + "Destino: " + tickets.Destino + "\n"  + "Valor de Tickets: $" + tickets.ValTicket + "\n" + "Fecha: " + tickets.Fecha,
            //     "OK");
            //}

        }
    }
}