
using EstadisticaApp.DataAcces.Implement;
using EstadisticaApp.DataAcces.Interfaces;

using EstadisticaApp.Utilities;
using System.Diagnostics;


namespace EstadisticaApp.Controllers
{
    public class ControlApiDBIngreso<TT> where TT :class,IModelsIngreso
    {

        private readonly ConsultaGeneral<TT> __context;

        private ApiRes<TT> apiRes;

        public ControlApiDBIngreso()
        {
            apiRes = new ApiRes<TT>();
            __context = new ConsultaGeneral<TT>();


        }
        //Implmrntar errores en la vista
       
        public bool BoolCount { set; get; } = false;

        public async Task<List<TT>> Getss() => await __context.Get();
        public async Task VerificarData()
        {
            BoolCount = __context.BoolCount();   
            
            Stopwatch timeMeasure = new Stopwatch();
            //Falta claseGeneral
            string[] rubros = { "01","02","03","04","05"};
            
            timeMeasure.Start();
            
            if (BoolCount)
            {
                foreach (var item in rubros){                
                
                            
                    Debug.WriteLine("Entra A la petición");
                    try
                    {
                        //Viene desde 
                        var presupuestoApi = await apiRes.GetsList("Ingresos",item);
                        Debug.WriteLine("Contador>>"+presupuestoApi.Count);

                        await __context.Insert(presupuestoApi);

                        await Task.CompletedTask;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.ToString());
                    }

                }
                
            }
            timeMeasure.Stop();
            Debug.WriteLine("Llamado de api min:>>>>>>"+timeMeasure.Elapsed.TotalSeconds/60 + "<<<<=Secgundos");
        }
        public async Task ClearTable() => await __context.ClearTAble();
        public async Task<List<double>> UnidadSuma() => await __context.UnidadSuma();

    }

}
