using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cootrasana.Models
{
    public class TicketsModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public int idOrigen { get; set; }
        public int idDestino { get; set; }
        public int NoPersonas { get; set; }
        public double ValTicket { get; set; }
        public bool Encomienda { get; set; }
        public bool Alert { get; set; }
        public string Hora { get; set; }
        public int idViaje { get; set; }
        public DateTime Fecha { get; set; }

        public TicketsModel()
        {

        }
    }
}
