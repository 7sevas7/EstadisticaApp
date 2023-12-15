


using EstadisticaApp.DataAcces.Implement;
using EstadisticaApp.DataAcces.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace EstadisticaApp.Models
{
    public class UnidadesIngresos :ClaseLimitadoraPadre, IModelsIngreso
    {
        [Key]
        public int IdUnidades { get; set; }
        public override string? Rubro { get; set; }
        public string? Unidad { get; set; }
        
        public string? Mes { get; set; }
        public double? Recaudado { get; set; }
    }
}
