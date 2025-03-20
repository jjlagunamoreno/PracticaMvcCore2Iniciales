using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PracticaMvcCore2Iniciales.Models
{
    [Table("PEDIDOS")]
    public class Pedido
    {
        [Key]
        [Column("IdPedido")]
        public int IdPedido { get; set; }
        [Column("IdFactura")]
        public int IdFactura { get; set; }
        [Column("Fecha")]
        public DateTime Fecha { get; set; }
        [Column("IdLibro")]
        public int IdLibro { get; set; }
        [Column("IdUsuario")]
        public int IdUsuario { get; set; }
        [Column("Cantidad")]
        public int Cantidad { get; set; }
    }
}
