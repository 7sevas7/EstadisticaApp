using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstadisticaApp.Models
{
    public class UnidadesPresupuesto
    {
        [Key]
        public int Id { get; set; }

        public int Id_Ppto { get; set; }
        public double? Egreso_Imp_aprobado { set; get; }
        public int? Num_Mes { set; get; }
        public double? egreso_Imp_Ampliacion { get; set; }
        public double? Egreso_Imp_Reduccion { get; set; }
        public double? imp_Modificado { get; set; }
        public double? Egreso_Imp_Comprometido { get; set; }
        public double? Egreso_Imp_Devengado { get; set; }
        public double? Egreso_Imp_Ejercido { get; set; }
        public double? Egreso_Imp_Pagado { get; set; }
        public double? Ejecutado { get; set; }
        public double? Imp_Comp_Dev_Eje_Pagado{set;get;}
 
        public string? Cve_Rubro_Ingreso { get; set; }


    }
}
