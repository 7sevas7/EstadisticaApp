
using EstadisticaApp.DataAcces.Implement;
using EstadisticaApp.DataAcces.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace EstadisticaApp.Models
{

    //Implmentara de otro lado 
    public class UnidadesPresupuesto :IModelsEgreso
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
     
        //public double? Recaudado { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //public double? Recaudado { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
