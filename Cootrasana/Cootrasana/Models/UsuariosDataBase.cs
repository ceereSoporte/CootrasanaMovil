using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Cootrasana.Models
{
    public class UsuariosDataBase
    {
        private SQLiteConnection conn;

        //CREATE  
        public UsuariosDataBase()
        {
            conn = DependencyService.Get<ConectionSQLite>().GetConnection();
            conn.CreateTable<UsuariosModel>();
        }

        //READ  
        public IEnumerable<UsuariosModel> GetMembers()
        {
            var members = (from mem in conn.Table<UsuariosModel>() select mem);
            return members.ToList();
        }

        //INSERT  
        public string AddMember(UsuariosModel Usuarios)
        {
            conn.Insert(Usuarios);
            return "success";
        }
        //DELETE  
        public string DeleteMember(int id)
        {
            conn.Delete<UsuariosModel>(id);
            return "success";
        }
        //DELETE DATOS
        public string DeleteTable()
        {
            conn.DeleteAll<UsuariosModel>();
            return "success";
        }
    }
}

