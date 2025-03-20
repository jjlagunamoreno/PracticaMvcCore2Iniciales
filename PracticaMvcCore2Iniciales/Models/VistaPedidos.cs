using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PracticaMvcCore2Iniciales.Models
{
    [Table("VISTAPEDIDOS")]
    public class VistaPedidos
    {
        [Key]
        [Column("IdVistaPedidos")]
        public int IdVistaPedidos { get; set; }
        [Column("IdUsuario")]
        public int IdUsuario { get; set; }
        [Column("Nombre")]
        public string Nombre { get; set; }
        [Column("Apellidos")]
        public string Apellidos { get; set; }
        [Column("Titulo")]
        public string Titulo { get; set; }
        [Column("Precio")]
        public int Precio { get; set; }
        [Column("Portada")]
        public string Portada { get; set; }
        [Column("Fecha")]
        public DateTime Fecha { get; set; }
        [Column("PrecioFinal")]
        public int PrecioFinal { get; set; }
    }
}
