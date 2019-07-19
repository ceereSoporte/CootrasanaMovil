namespace Cootrasana.Models
{
    using SQLite;

    public class LoginModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string name { get; set; }
        public string Password { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }

        public LoginModel()
        {

        }
    }
}
