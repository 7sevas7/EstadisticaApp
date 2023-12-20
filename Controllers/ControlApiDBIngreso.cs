
using EstadisticaApp.DataAcces;
using EstadisticaApp.DataAcces.Implement;

using EstadisticaApp.Models;
using EstadisticaApp.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;


namespace EstadisticaApp.Controllers
{
    public class ControlApiDBIngreso<TT> where TT : UnidadesIngresos
    {

        private readonly ConsultaGeneral<TT> __context;
        
        private readonly DBContext contexIn;

        private ApiRes<TT> apiRes;

        public bool borrarT { set; get; }

        public ControlApiDBIngreso()
        {
            apiRes = new ApiRes<TT>();
            __context = new ConsultaGeneral<TT>();

            contexIn = new DBContext();


        }
        //Implmrntar errores en la vista
       
        public bool BoolCount { set; get; } = false;

        public async Task<List<TT>> Getss() => await __context.Get();
        public async Task VerificarData()
        {
            if (borrarT) {
                await __context.ClearTAble();
            }

            BoolCount = __context.BoolCount();   
            
            Stopwatch timeMeasure = new Stopwatch();
            //Falta claseGeneral
            string[] rubros = { "01","02","03","04","05"};
            Debug.WriteLine("<>>>>>>>>>>"+BoolCount);
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
            Debug.WriteLine("Llamado de api min:>>>>>>"+timeMeasure.Elapsed.TotalMinutes + "<<<<=Segundos");
        }
        public async Task ClearTable() => await __context.ClearTAble();
        //public async Task<List<double>> UnidadSuma() => await UnidadSuma();

        //Esta funcion sera asi ya que aun no se soluciona para hacer la consulta de manera mas general
        public  async Task<List<double>> UnidadSuma()
        {
            List<double> listSuma = new List<double>();
            foreach (var item in Rubros)
            {
                var uni = await contexIn.Set<TT>().Where( U => U.Rubro == item).SumAsync(r => r.Recaudado);
                    
                listSuma.Add((double)uni);
            }

            return listSuma;
        }


        List<string> Rubros = new List<string> { "01", "02", "03", "04", "05" };

    }

}
