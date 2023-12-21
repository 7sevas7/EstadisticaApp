﻿

using EstadisticaApp.DataAcces.Implement;
using EstadisticaApp.Models;
using EstadisticaApp.Utilities;
using System.Diagnostics;

namespace EstadisticaApp.Controllers
{
    public class ControlApiDBEgreso<TT> where TT : UnidadesPresupuesto
    {
        private PresupuestoMain<UnidadesPresupuesto> presupuestoMain = new PresupuestoMain<UnidadesPresupuesto>();
        
        private readonly ConsultaGeneral<TT> __context;
        
        public bool borrarT { set; get; }

        private ApiRes<TT> apiRes;
        //public bool BoolCount { set; get; } = false;


        public ControlApiDBEgreso()
        {
            apiRes = new ApiRes<TT>();
            __context = new ConsultaGeneral<TT>();


        }
        //Datos para graficas
        public async Task<List<UnidadesPresupuesto?>> AcumuladoUnidad() => await presupuestoMain.Get();
        //Datos de manera general
        public async Task<List<double?>> AcumuladoIngresos() => await presupuestoMain.AcumuladoIngresos();

        //Datos pormes y rubro
        public async Task<List<UnidadesPresupuesto>>? UnidadXMeses(string rubro) => await presupuestoMain.UnidadXMeses(rubro);

        
        public async Task VerificarData()
        {
            if (borrarT)
            {
                await __context.ClearTAble();
            }

           // BoolCount = __context.BoolCount();

            Stopwatch timeMeasure = new Stopwatch();
            //Falta claseGeneral
            string[] rubros = { "01", "02", "03", "04", "05" };
            Debug.WriteLine("Entra con click");
            //Esta mal el manejo de errores 

               // await __context.ClearTAble();
            
            timeMeasure.Start();

           // if (BoolCount)
            //{

                foreach (var item in rubros)
                {


                    Debug.WriteLine("Entra A la petición");
                    try
                    {
                        
                        //Viene desde 
                        var presupuestoApi = await apiRes.GetsList("Egresos", item);
                        
                        Debug.WriteLine("Contador>>" + presupuestoApi.Count);
                        
                        await __context.Insert(presupuestoApi);

                        await Task.CompletedTask;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.ToString());
                    }

              //  }

            }
            timeMeasure.Stop();
            Debug.WriteLine("Llamado de api min:>>>>>>" + timeMeasure.Elapsed.TotalSeconds / 60 + "<<<<=Secgundos");
        }
        
        //Tal vez se ocuape más adelante 
        public async Task<List<double?>> EgresoUnidadData(string tipo)
        {
            var acumulado = await AcumuladoUnidad();
            return acumulado.Select(u => (double?)typeof(TT).GetProperty(tipo).GetValue(u)).ToList();
        }
        public async Task insertApi()
        {

            Stopwatch timeMeasure = new Stopwatch();
            //Falta claseGeneral
            string[] rubros = { "01", "02", "03", "04", "05" };


            foreach (var item in rubros)
            {
                Debug.WriteLine("Entra A la petición");
                try
                {
                    //Viene desde 
                    var presupuestoApi = await apiRes.GetsList("Ingresos", item);
                    Debug.WriteLine("Contador>>" + presupuestoApi.Count);

                    await __context.Insert(presupuestoApi);


                }
                catch (Exception ex)
                {
                    //Verficar el error al insertar
                    throw new Exception(ex.Message);
                }
            }
        }
        public async Task Verificar()
        {
            //Verficar que se quiere hacer en caso que se quiera borrar 
            //Verficar la conexion 
            if (borrarT)
            {
                var conexion = apiRes.verificar();
                if (conexion)
                {
                    await __context.ClearTAble();
                    await insertApi();
                }
                else
                {
                    throw new Exception("Error de conexión");
                }
            }

        }
        public async Task<string[]> Meses() => await presupuestoMain.Meses();
    }
}
