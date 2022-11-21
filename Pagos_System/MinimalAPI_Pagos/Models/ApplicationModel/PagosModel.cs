using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalAPI_Pagos.Models.ApplicationModel
{
    public class PagosModel
    {
        [Key, Required]
        public int IdFactura { get; set; }

        [MaxLength(10), Column(TypeName = "nvarchar"), Required]
        public string Patente { get; set; }

        [MaxLength(10), Column(TypeName = "money"), Required]
        public string Monto { get; set; }

        [Column(TypeName = "datetime"), Required]
        public DateTime Fecha { get; set; }

        [Column(TypeName = "bit"), Required]
        public bool Active { get; set; }
    }
}
