
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
    }
}
