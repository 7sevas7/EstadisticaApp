using EstadisticaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstadisticaApp.DataAcces.Interfaces
{
    public interface IPresupuestoMain
    {
        Task InsertPresupuestos(List<UnidadesPresupuesto> presupuestos);
//        List<UnidadesPresupuesto> UnidadXMes(string rubro);


    }
}
