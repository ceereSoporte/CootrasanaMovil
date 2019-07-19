using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cootrasana.Models
{
    public class UsuariosModel
    {
        [PrimaryKey, AutoIncrement]
        public int IdUsuarioSistema { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int Cedula { get; set; }
        public int Telefono { get; set; }
        public int Celular { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public DateTime FechaRegistroPropietario { get; set; }
        public int IdTipoIdentificacion { get; set; }
        public bool Estado { get; set; }


        public UsuariosModel()
        {

        }
    }
}
