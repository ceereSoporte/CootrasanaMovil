using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cootrasana.Models
{
    public class ViajesModel
    {
        [PrimaryKey]
        public int id { get; set; }
        public int idOrigen { get; set; }
        public int idDestino { get; set; }
        public string origen { get; set; }
        public string destino { get; set; }
        public string nombre { get; set; }
        public int valor { get; set; }
        public string Hora { get; set; }

        public ViajesModel()
        {

        }
    }
}
