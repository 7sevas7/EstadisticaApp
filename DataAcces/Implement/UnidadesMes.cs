using EstadisticaApp.DataAcces.Interfaces;

using EstadisticaApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EstadisticaApp.DataAcces.Implement
{
    public class UnidadesMes : IUnidadesMes
    {
        private readonly DBContext __context;
        public async Task<string[]> mesesUpper()
        {
            var mesesUp = await Meses();
            string[] mese = mesesUp.ToArray();
            mese = mese.Select(it => it.ToUpper()).ToArray();
            return mese;
        }
        
        public UnidadesMes()
        {
            __context = new DBContext();

        }
        //Generara problemas sobre todo si aun no se toma en cuanta el año            
        public async Task<List<UnidadesIngresos>> GetMesePorUnidad(string rubro)
        {
            List<UnidadesIngresos> listaXMes = new List<UnidadesIngresos>();
            var meses = await Meses();
            foreach (var item in meses)
            {
                var recaudadoMes = await RecaudadoMes(item,rubro);
                listaXMes.Add(recaudadoMes);
                
            }
            // Utiliza Sum() para calcular la suma de una columna específica
           // NuevaColumna = __context.OtraTabla.Where(ot => ot.RelatedId == mes.Id).Sum(ot => ot.ColumnaASumar)
            return listaXMes;
            
        }
        public async Task<List<UnidadesIngresos>> GetsUnidades() {            
                return await __context.UnidadesIngreso.ToListAsync();           
        }
        public async Task<List<string>>? Meses()
        {
            var listaMeses = await __context.UnidadesIngreso.Select(mes => mes.Mes).Distinct().ToListAsync();
            return  listaMeses;         
        }

        private async Task<UnidadesIngresos>? RecaudadoMes(string Mes,string rubro)
        {
            UnidadesIngresos? unidadesMes = await __context.UnidadesIngreso
                .Where(mes => mes.Mes == Mes && mes.Rubro == rubro)
                .Select(mes => new UnidadesIngresos{
                    // Asigna las propiedades específicas de Unidades
                    IdUnidades = mes.IdUnidades,
                    Mes = mes.Mes,
                    Unidad = mes.Unidad,
                    Rubro = mes.Rubro,
                    Recaudado =  __context.UnidadesIngreso.Where(ot => ot.Rubro == rubro && ot.Mes == Mes).Sum(ot => ot.Recaudado),          
                }).FirstOrDefaultAsync();
            
            return unidadesMes;
        }

    }
}
