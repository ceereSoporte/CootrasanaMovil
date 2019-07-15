

namespace Cootrasana.Backend.Models
{
    using Cootrasana.Domain.Model;

    public class LocalContext : DataContext
    {
        public System.Data.Entity.DbSet<Cootrasana.Common.Models.Tickets_Movil> Tickets_Movil { get; set; }
    }
}