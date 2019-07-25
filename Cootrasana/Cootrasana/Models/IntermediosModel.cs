using System;
using System.Collections.Generic;
using System.Text;

namespace Cootrasana.Models
{
    public class IntermediosModel
    {
        public int id { get; set; }
        public int idOrigen { get; set; }
        public int idDestino { get; set; }
        public string origen { get; set; }
        public string destino { get; set; }
        public int valor { get; set; }

        public IntermediosModel()
        {

        }
    }
}
