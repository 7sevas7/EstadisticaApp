
namespace EstadisticaApp.DataAcces.Interfaces
{
    public interface IConsultasGeneral<Unidadd>
    {
       Task<List<Unidadd>> Get();
       Task Insert(List<Unidadd> listRange);
       Task<List<double>> UnidadSuma();
       Task ClearTAble();
        // Verificar conteo
       bool BoolCount();
    }
    public interface IModelsIngreso
    {
        double? Recaudado { set; get; }
        
    }
    public interface IModelsEgreso  {
        public string? Rubro { get; set; }
    }
   
}
