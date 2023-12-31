﻿using EstadisticaApp.DataAcces.Interfaces;
using EstadisticaApp.Models;
using EstadisticaApp.Utilities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq.Dynamic.Core;


namespace EstadisticaApp.DataAcces.Implement
{
    public class PresupuestoMain : IPresupuestoMain
    {

        private DBContext __context;
        

        public List<UnidadesPresupuesto> UnidadXMes { set; get; }

        public PresupuestoMain()
        {
            __context = new DBContext();
            
        }
        //Recordemos que entodo esta parte podria ri la implementacion de la memeoria cache 
        //Esta funcion los datos bentran desde al api que se generara 
        public async Task InsertPresupuestos (List<UnidadesPresupuesto>? presupuestos)
        {
            List<UnidadesPresupuesto> listaInstert = new();
            foreach (var item in presupuestos)
            {
                listaInstert.Add(item);
                if (listaInstert.Count == 300) {
                    await __context.UnidadesPresupuesto.AddRangeAsync(listaInstert);
                    await __context.SaveChangesAsync();
                    listaInstert.Clear();
                    __context.ChangeTracker.Clear();
                }
            }
            await __context.UnidadesPresupuesto.AddRangeAsync(listaInstert);
            await __context.SaveChangesAsync();
            listaInstert.Clear();
            __context.ChangeTracker.Clear();

            Debug.Write("============>Listo!!!>>>>>>>>>>>>");
            
        }
        public async Task<List<UnidadesPresupuesto>>? GetUnidadesPresupuesto()
        {
            return await __context.UnidadesPresupuesto.ToListAsync();
            
        }

        //Funcion de prueba para la insercion se datos 
        public async Task InserPruebaComment()
        {                                     
            using var stream = await FileSystem.OpenAppPackageFileAsync("EgresosMes.json");
            using var reader = new StreamReader(stream);
            var ll = await reader.ReadToEndAsync();
            var ListaPresupuesto = JsonConvert.DeserializeObject<List<UnidadesPresupuesto>>(ll);            
            await InsertPresupuestos(ListaPresupuesto);
            
            
        }
        //Solo datos de prueba
        public async Task InserPrueba()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("EgresosMes.json");
            var serial = new JsonSerializer();
            List<UnidadesPresupuesto> insert = new List<UnidadesPresupuesto>();
            using (var reader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(reader))
            {
                while (jsonReader.Read()) {
                    if (jsonReader.TokenType == JsonToken.StartObject) {
                        var obj = serial.Deserialize<UnidadesPresupuesto>(jsonReader);
                        insert.Add(obj);
                        if (insert.Count == 100) {
                            await __context.UnidadesPresupuesto.AddRangeAsync(insert);
                            __context.SaveChanges();
                            insert.Clear();
                            __context.ChangeTracker.Clear();
                        }
                        
                    }
                }
              
            }

            await __context.UnidadesPresupuesto.AddRangeAsync(insert);
            __context.SaveChanges();
            __context.ChangeTracker.Clear();


        }

        //
        //Esta funcion se podran añadir todos los demas sumas de Presupuestos// Se definira mas adelante en otra grafica //Exactamente es solo un un egreso pero no se cual por el momento 
        public async Task<List<UnidadesPresupuesto>> AcumuladoUnidad() {
            //Sera con un for para su modificacion por cada unidad
            //Se añadira el mes más adelante 
            List<UnidadesPresupuesto> acumulado = new();
            foreach (var rubro in RubroList())
            {
            UnidadesPresupuesto? presupuestoAcumulado = await __context.UnidadesPresupuesto
                .Where(u => u.Cve_Rubro_Ingreso.Substring(2, 2) == rubro)
                .Select(pre => new UnidadesPresupuesto
                {
                    Cve_Rubro_Ingreso = pre.Cve_Rubro_Ingreso.Substring(2, 2),
                    Num_Mes = pre.Num_Mes,
                    Egreso_Imp_aprobado = __context.UnidadesPresupuesto.Where(u => u.Cve_Rubro_Ingreso.Substring(2, 2) == rubro).Sum(u => u.Egreso_Imp_aprobado),
                    egreso_Imp_Ampliacion = __context.UnidadesPresupuesto.Where(u => u.Cve_Rubro_Ingreso.Substring(2, 2) == rubro).Sum(u => u.egreso_Imp_Ampliacion),
                    Egreso_Imp_Reduccion = __context.UnidadesPresupuesto.Where(u => u.Cve_Rubro_Ingreso.Substring(2, 2) == rubro).Sum(u => u.Egreso_Imp_Reduccion),
                    imp_Modificado = __context.UnidadesPresupuesto.Where(u => u.Cve_Rubro_Ingreso.Substring(2, 2) == rubro).Sum(u => u.imp_Modificado),
                    Imp_Comp_Dev_Eje_Pagado = __context.UnidadesPresupuesto.Where(u => u.Cve_Rubro_Ingreso.Substring(2, 2) == rubro).Sum(u => u.Imp_Comp_Dev_Eje_Pagado)

                })
                .FirstOrDefaultAsync();//Añadir ciclo mes !!
                acumulado.Add(presupuestoAcumulado);

        }
            return acumulado;
        }
        //Lista que debuelve cada recaudo por unida, pero en cada objeto la suma de tal
        public async Task UnidadXMeses(string rubro)
        {
            //Sera con un for para su modificacion por cada unidad
            //Se añadira el mes más adelante 
            List<UnidadesPresupuesto>? listaXMEs = new();
            
            var mes = await MesesN();
            foreach (var item in mes)
            {
                var mesUnidad = await  UnidadMes(rubro, item);
                listaXMEs.Add(mesUnidad);
            }
            UnidadXMes =  listaXMEs.OrderBy(mes => mes.Num_Mes).ToList();
        }
        //Devuleve una lista de recaudo de un tipo con el subro establecido
        //Devuelve un objeto son sus respectivas sumas 
        public async Task<UnidadesPresupuesto>? UnidadMes(string rubro,int? mes )
        {
            UnidadesPresupuesto? presupuestoMes = await __context.UnidadesPresupuesto
              .Where(u => u.Cve_Rubro_Ingreso.Substring(2, 2) == rubro && u.Num_Mes == mes)
              .Select(pre => new UnidadesPresupuesto
              {
                  Cve_Rubro_Ingreso = pre.Cve_Rubro_Ingreso,
                  Num_Mes = pre.Num_Mes,
                  Egreso_Imp_aprobado = __context.UnidadesPresupuesto.Where(u => u.Cve_Rubro_Ingreso.Substring(2, 2) == rubro && u.Num_Mes == mes).Sum(u => u.Egreso_Imp_aprobado),
                  egreso_Imp_Ampliacion = __context.UnidadesPresupuesto.Where(u => u.Cve_Rubro_Ingreso.Substring(2, 2) == rubro && u.Num_Mes == mes).Sum(u => u.egreso_Imp_Ampliacion),
                  Egreso_Imp_Reduccion = __context.UnidadesPresupuesto.Where(u => u.Cve_Rubro_Ingreso.Substring(2, 2) == rubro && u.Num_Mes == mes).Sum(u => u.Egreso_Imp_Reduccion),
                  imp_Modificado = __context.UnidadesPresupuesto.Where(u => u.Cve_Rubro_Ingreso.Substring(2, 2) == rubro && u.Num_Mes == mes).Sum(u => u.imp_Modificado),
                  Imp_Comp_Dev_Eje_Pagado = __context.UnidadesPresupuesto.Where(u => u.Cve_Rubro_Ingreso.Substring(2, 2) == rubro && u.Num_Mes == mes).Sum(u => u.Imp_Comp_Dev_Eje_Pagado)
              })
              .FirstOrDefaultAsync();//Añadir ciclo mes !!
            var count = __context.UnidadesPresupuesto.Count();
            return presupuestoMes;
        }
        private async Task<List<int?>> MesesN()
        {
            List<int?> listaM = new List<int?>();
            listaM = await __context.UnidadesPresupuesto.Select(u => u.Num_Mes).Distinct().ToListAsync();
            
            return listaM;
        }
       //Para organizar la lista de meses
        public async Task<string[]> Meses()
        {
            List<string> meses = new();
            var mesesN = await MesesN();
            var ordenado = mesesN.OrderBy(x => x.Value);
            foreach (var mes in ordenado) {
                meses.Add(idenMes(mes));   
            }
            return meses.ToArray();
        }
        //Simplemente para los subros he iterar 
        public List<string> RubroList()
        {
            return new List<string> {"01","02","03","04","05"};

        }
        
        
        private string idenMes(int? mesInt) {
            switch (mesInt) {
                case 1:
                    return "ENERO";
                case 2:
                    return "FEBREO";
                case 3:
                    return "MARZO";
                case 4:
                    return "ABRIL";
                case 5:
                    return "MAYO";
                case 6:
                    return "JUNIO";
                case 7:
                    return "JULIO";
                case 8:
                    return "AGOSTO";
                    case 9:
                    return "SEPTIEMBRE";
                case 10:
                    return "OCTUBRE";
                case 11:
                    return "NOVIEMBRE";
                case 12:
                return "DICIEMBRE";
                    default:
                    return "--";
            }
           
        }
        //Simplemente es la suma para diferenciar con los ingresados
        public async Task<List<double?>> AcumuladoIngresos()
        {
            List<double?> ret = new List<double?>();
            foreach (var item in RubroList())
            {
                var suma = await __context.UnidadesIngreso.Where(u => u.Rubro == item).SumAsync(r => r.Recaudado);
                ret.Add(suma);
            }
            return ret;
        }
        public async Task ClearTable() {
            await __context.UnidadesPresupuesto.ExecuteDeleteAsync();
        }
        //Verificación si la tabla esta vacia 
        public bool BoolCount() => __context.UnidadesPresupuesto.Count() > 0 ? false : true;
    }
}
