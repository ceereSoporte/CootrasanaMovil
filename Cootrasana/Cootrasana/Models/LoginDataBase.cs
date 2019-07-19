
namespace Cootrasana.Models
{
    using SQLite;
    using System.Collections.Generic;
    using System.Linq;
    using Xamarin.Forms;

    public class LoginDataBase
    {
        private SQLiteConnection conn;

        //CREATE  
        public LoginDataBase()
        {
            conn = DependencyService.Get<ConectionSQLite>().GetConnection();
            conn.CreateTable<LoginModel>();
        }

        //READ  
        public IEnumerable<LoginModel> GetMembers()
        {
            var members = (from mem in conn.Table<LoginModel>() select mem);
            return members.ToList();
        }
        public bool GetMemberLogin(string name, string password)
        {
            var member = (from mem in conn.Table<LoginModel>() where mem.name == name && mem.Password == password select mem);
            var lis = member.ToList();
            if (lis.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //INSERT  
        public string AddMember(LoginModel login)
        {
            conn.Insert(login);
            return "success";
        }
        //DELETE  
        public string DeleteMember(int id)
        {
            conn.Delete<LoginModel>(id);
            return "success";
        }
        //DELETE DATOS
        public string DeleteTable()
        {
            conn.DeleteAll<LoginModel>();
            return "success";
        }
    }
}
