using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cootrasana.Models
{
    public class Tickets
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public int NoPersonas { get; set; }
        public int ValTicket { get; set; }
        public bool Encomienda { get; set; }

        public Tickets()
        {

        }
    }
}
