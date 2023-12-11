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

            var count = presupuestoMain.BoolCount();

            var presupuestoApi = await GetPresupuestoApi();
            if (!count) {
                Debug.Write("Guardandoww============>>>>>>>>");
                try
                {
                    Debug.Write("Guardando============>>>>>>>>");
                    await presupuestoMain.InsertPresupuestos(presupuestoApi);                    
                    await Task.CompletedTask;
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.ToString());
                }
              
            }
          
          


        }
        public async Task<List<UnidadesPresupuesto>?> GetPresupuestoApi()=> await apiRes.GetsListPresupuesto();

    }

}
