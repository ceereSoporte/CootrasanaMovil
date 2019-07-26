namespace Cootrasana.Models
{
    using SQLite;
    using System.Collections.Generic;
    using System.Linq;
    using Xamarin.Forms;

    public class UbicacionesDataBase
    {
        private SQLiteConnection conn;

        //CREATE  
        public UbicacionesDataBase()
        {
            conn = DependencyService.Get<ConectionSQLite>().GetConnection();
            conn.CreateTable<UbicacionesModel>();
        }

        //READ  
        public IEnumerable<UbicacionesModel> GetMembers()
        {
            var members = (from mem in conn.Table<UbicacionesModel>() select mem);
            return members.ToList();
        }

        //INSERT  
        public string AddMember(UbicacionesModel ubicaciones)
        {
            conn.Insert(ubicaciones);
            return "success";
        }
        //DELETE  
        public string DeleteMember(int id)
        {
            conn.Delete<UbicacionesModel>(id);
            return "success";
        }
        //DELETE DATOS
        public string DeleteTable()
        {
            conn.DeleteAll<UbicacionesModel>();
            return "success";
        }
    }
}
