using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Cootrasana.Models
{
    public class TicketsDataBase
    {
        private SQLiteConnection conn;

        //CREATE  
        public TicketsDataBase()
        {
            conn = DependencyService.Get<ConectionSQLite>().GetConnection();
            conn.CreateTable<TicketsModel>();
        }

        //READ  
        public IEnumerable<TicketsModel> GetMembers()
        {
            var members = (from mem in conn.Table<TicketsModel>() select mem);
            return members.ToList();
        }
        //INSERT  
        public string AddMember(TicketsModel tickets)
        {
            conn.Insert(tickets);
            return "success";
        }
        //DELETE  
        public string DeleteMember(int id)
        {
            conn.Delete<TicketsModel>(id);
            return "success";
        }
    }
}

