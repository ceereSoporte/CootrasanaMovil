using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Cootrasana.Models
{
    public class ViajesDataBase
    {
        private SQLiteConnection conn;

        //CREATE  
        public ViajesDataBase()
        {
            conn = DependencyService.Get<ConectionSQLite>().GetConnection();
            conn.CreateTable<ViajesModel>();
        }

        //READ  
        public IEnumerable<ViajesModel> GetMembers()
        {
            var members = (from mem in conn.Table<ViajesModel>() select mem);
            return members.ToList();
        }
        
        //INSERT  
        public string AddMember(ViajesModel viaje)
        {
            conn.Insert(viaje);
            return "success";
        }

        //READ ONEMEMBER
        public IEnumerable<ViajesModel> GetOneMembers(int id)
        {
            var members = (from mem in conn.Table<ViajesModel>() select mem).Where(mem => mem.id == id);
            return members.ToList();
        }

        //DELETE  
        public string DeleteMember(int id)
        {
            conn.Delete<ViajesModel>(id);
            return "success";
        }
        //DELETE DATOS
        public string DeleteTable()
        {
            conn.DeleteAll<ViajesModel>();
            return "success";
        }
    }
}

