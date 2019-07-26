using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Cootrasana.Models
{
    public class IntermediosDataBase
    {
        private SQLiteConnection conn;

        //CREATE  
        public IntermediosDataBase()
        {
            conn = DependencyService.Get<ConectionSQLite>().GetConnection();
            conn.CreateTable<IntermediosModel>();
        }

        //READ  
        public IEnumerable<IntermediosModel> GetMembers()
        {
            var members = (from mem in conn.Table<IntermediosModel>() select mem);
            return members.ToList();
        }

        public IEnumerable<IntermediosModel> GetOneMembers(int id)
        {
            var members = (from mem in conn.Table<IntermediosModel>() select mem).Where(i => i.idOrigen == id);
            return members.ToList();
        }

        public IEnumerable<IntermediosModel> GetVal(int idOrigen, int idDestino)
        {
            var members = (from mem in conn.Table<IntermediosModel>() select mem).Where(i => i.idOrigen == idOrigen && i.idDestino == idDestino);
            return members.ToList();
        }
        
        //INSERT  
        public string AddMember(IntermediosModel intermedio)
        {
            conn.Insert(intermedio);
            return "success";
        }
        //DELETE  
        public string DeleteMember(int id)
        {
            conn.Delete<IntermediosModel>(id);
            return "success";
        }
        //DELETE DATOS
        public string DeleteTable()
        {
            conn.DeleteAll<IntermediosModel>();
            return "success";
        }
    }
}
