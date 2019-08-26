using System;
using System.Collections.Generic;
using System.Text;

namespace Cootrasana.Models
{
    public class UsuariosViajeModel
    {
        public int id { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public int documento { get; set; }
        public int Puesto { get; set; }
        public int ticket { get; set; }
    }
}
