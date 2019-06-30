namespace Cootrasana.Models
{
    using SQLite;

    public class LoginModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }

        public LoginModel()
        {

        }
    }
}
