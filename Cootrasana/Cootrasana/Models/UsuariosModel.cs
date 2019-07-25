using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cootrasana.Models
{
    public class UsuariosModel
    {
        public int id { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string user { get; set; }
        public string password { get; set; }


        public UsuariosModel()
        {

        }
    }
}
