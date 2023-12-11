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

                    throw new Exception(ex.ToString());
                }

            }
            timeMeasure.Stop();
            Debug.WriteLine("Guardar Datos==>>" + timeMeasure.Elapsed.TotalSeconds);



        }


    }

}
