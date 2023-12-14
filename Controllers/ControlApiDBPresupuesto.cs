using EstadisticaApp.DataAcces.Implement;
using EstadisticaApp.Models;
using EstadisticaApp.Utilities;

using System.Diagnostics;


namespace EstadisticaApp.Controllers
{
    public class ControlApiDBPresupuesto
    {
        //Api
        private ApiRes apiRes;
        //PresupuestoMain
        PresupuestoMain presupuestoMain;
        public ControlApiDBPresupuesto()
        {
            apiRes = new ApiRes();
            presupuestoMain = new PresupuestoMain();


        }
<<<<<<< HEAD
        public async Task verificarCount() {
            Stopwatch timeMeasure = new Stopwatch();
         
                var count = presupuestoMain.BoolCount();

            timeMeasure.Start();
            if (count)
            {
                List<UnidadesPresupuesto>? listOfApi = await apiRes.GetsListPresupuesto();
                Debug.Write("guardandoww============>>>>>>>>");
                try
                {
                    Debug.Write("guardando============>>>>>>>>");
                    await presupuestoMain.InsertPresupuestos(listOfApi);
                    
                }
                catch (Exception ex)
                {
=======
        public async Task verificarCount()
        {
            Debug.WriteLine( "<<<<<<<<<<<<<<<<<<<<");
            Stopwatch timeMeasure = new Stopwatch();

            var count = presupuestoMain.BoolCount();
            string[] rubros = { "01","02","03","04","05"};
            
            if (count)
            {
                foreach (var item in rubros){                
                
                timeMeasure.Start();                
                    Debug.WriteLine("Entra A la petición");
                    try
                    {
                        var presupuestoApi = await apiRes.GetsListPresupuesto(item);
                        Debug.WriteLine("Contador>>"+presupuestoApi.Count);
                        await presupuestoMain.InsertPresupuestos(presupuestoApi);

                        await Task.CompletedTask;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.ToString());
                    }
>>>>>>> PruebasIngreso

                }
<<<<<<< HEAD

            }
            timeMeasure.Stop();
            Debug.WriteLine("Guardar Datos==>>" + timeMeasure.Elapsed.TotalSeconds);



        }

=======
                timeMeasure.Stop();
                Debug.WriteLine(timeMeasure.Elapsed.TotalSeconds+"<<<<=Secgundos");
            }

        }
        
>>>>>>> PruebasIngreso

    }

}
