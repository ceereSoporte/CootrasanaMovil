using System;
using System.Collections.Generic;
using System.Text;

namespace Cootrasana.Models
{
    public class GeneralModel
    {
        public int id { get; set; }
        public int idOrigen { get; set; }
        public int idDestino { get; set; }
        public string origen { get; set; }
        public string destino { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string user { get; set; }
        public string nombre { get; set; } // Nombre de la Ruta
        public string password { get; set; }
        public int valor { get; set; }
        public string horaViaje { get; set; } 
    }
}
