


using System.ComponentModel.DataAnnotations;

namespace EstadisticaApp.Models
{
    public class UnidadesIngresos
    {
        [Key]
        public int IdUnidades { get; set; }
        public string? Rubro { get; set; }
        public string? Unidad { get; set; }
        public double? Recaudado { get; set; }
        public string? Mes { get; set; }

    }
}
