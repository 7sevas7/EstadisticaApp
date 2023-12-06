
using EstadisticaApp.Models;


namespace EstadisticaApp.DataAcces.Interfaces
{
    public interface IUnidadesMes 
    {
        Task<List<UnidadesIngresos>> GetMesePorUnidad(string rubro);

    }
}
