

namespace Cootrasana.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Tickets_Movil
    {
        
        [Key]
        public int idTickets { get; set; }

        [Required]
        public string Origen { get; set; }

        [Required]
        public string Destino { get; set; }

        [Required]
        public int NoPersonas { get; set; }

        [Required]
        public Decimal ValTickets { get; set; }

        [Required]
        public bool Encomienda { get; set; }

        public DateTime Fecha { get; set; }
    }
}
