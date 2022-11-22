using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalAPI_Pagos.Models.ApplicationModel
{
    public class PagosModel
    {
        public int IdFactura { get; set; }
        public string Patente { get; set; }
        public double Monto { get; set; }
        public DateTime Fecha { get; set; }
        public bool Active { get; set; }
    }
}
