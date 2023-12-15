using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstadisticaApp.DataAcces.Interfaces
{
    public interface IConsultasGeneral<Unidadd>
    {
       Task<List<Unidadd>> Get();
       Task Insert(List<Unidadd> listRange);
       Task<List<double>> UnidadSuma();
       Task ClearTAble();
    }
    public interface IModels
    {
        string Rubro { set; get; }
        double? Recaudado { set; get; }
    }

}
