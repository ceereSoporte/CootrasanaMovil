namespace Cootrasana.Models
{
    using SQLite;

    public class LoginModel
    {
        public int id { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string user { get; set; }
        public string password { get; set; }

        public LoginModel()
        {

        }
    }
}
