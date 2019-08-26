using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Cootrasana.Models
{
    public class UsuariosViajeDataBase
    {
        private SQLiteConnection conn;

        //CREATE  
        public UsuariosViajeDataBase()
        {
            conn = DependencyService.Get<ConectionSQLite>().GetConnection();
            conn.CreateTable<UsuariosViajeModel>();
        }

        //READ  
        public IEnumerable<UsuariosViajeModel> GetMembers()
        {
            var members = (from mem in conn.Table<UsuariosViajeModel>() select mem);
            return members.ToList();
        }

        public IEnumerable<UsuariosViajeModel> GetOneMembers(int ticket)
        {
            var members = (from mem in conn.Table<UsuariosViajeModel>() select mem).Where(i => i.ticket == ticket);
            return members.ToList();
        }

        //INSERT  
        public string AddMember(UsuariosViajeModel UsuariosViaje)
        {
            conn.Insert(UsuariosViaje);
            return "success";
        }
        //DELETE  
        public string DeleteMember(int id)
        {
            conn.Delete<UsuariosViajeModel>(id);
            return "success";
        }
        //DELETE DATOS
        public string DeleteTable()
        {
            conn.DeleteAll<UsuariosViajeModel>();
            return "success";
        }
    }
}
