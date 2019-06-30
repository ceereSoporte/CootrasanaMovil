using System;
using System.Collections.Generic;
using System.Text;

namespace Cootrasana.ViewModel
{
    public class MainViewModel
    {
        public TicketsViewModel Tickets { get; set; }

        public MainViewModel()
        {
            this.Tickets = new TicketsViewModel();
        }
    }
}
