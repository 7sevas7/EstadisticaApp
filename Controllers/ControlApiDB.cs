
using EstadisticaApp.DataAcces.Implement;
using EstadisticaApp.DataAcces.Interfaces;

using EstadisticaApp.Utilities;
using System.Diagnostics;


namespace EstadisticaApp.Controllers
{
    public class ControlApiDB<TT> where TT :class
    {

        private readonly ConsultaGeneral<TT> presupuestoMain;

        private ApiRes<TT> apiRes;

        public ControlApiDB()
        {
            apiRes = new ApiRes<TT>();
            presupuestoMain = new ConsultaGeneral<TT>();


        }
        public bool BoolCount { set; get; } = false;
        public async Task verificarCount()
        {
            BoolCount = presupuestoMain.BoolCount();   
            
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
                        var presupuestoApi = await apiRes.GetsListPresupuesto(item);
                        Debug.WriteLine("Contador>>"+presupuestoApi.Count);

                        await presupuestoMain.Insert(presupuestoApi);

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
        public async Task ClearTable() => presupuestoMain.ClearTAble();
        

    }

}
