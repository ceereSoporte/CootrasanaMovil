using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Cootrasana.Models
{
    public class CRUD
    {
        private SQLiteConnection conn;

        //CREATE  
        public CRUD()
        {
            conn = DependencyService.Get<ConectionSQLite>().GetConnection();
            conn.CreateTable<Tickets>();
        }

        //READ  
        public IEnumerable<Tickets> GetMembers()
        {
            var members = (from mem in conn.Table<Tickets>() select mem);
            return members.ToList();
        }
        //INSERT  
        public string AddMember(Tickets tickets)
        {
            conn.Insert(tickets);
            return "success";
        }
        //DELETE  
        public string DeleteMember(int id)
        {
            conn.Delete<Tickets>(id);
            return "success";
        }
    }
}

