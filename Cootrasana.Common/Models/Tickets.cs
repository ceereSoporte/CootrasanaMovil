namespace Cootrasana.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Tickets
    {
        [Key]
        public int idTicket { get; set; }

        [Required]
        public string Origen { get; set; }

        [Required]
        public string Destino { get; set; }

        [Required]
        public int NoPersonas { get; set; }

        [Required]
        public Decimal ValTicket { get; set; }

        [Required]
        public bool Encomienda { get; set; }

        [Required]
        public DateTime Fecha { get; set; }
    }
}
