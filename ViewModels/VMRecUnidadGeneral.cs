using EstadisticaApp.DataAcces;

using EstadisticaApp.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using EstadisticaApp.ConfigViewModel.Config;
using CommunityToolkit.Mvvm.Input;


namespace EstadisticaApp.Components.Pages
{
    public partial class VMRecUnidadGeneral : VMBase
    {
        protected DBContext __context = new DBContext();

        public string Mensaje { set; get; }
        public List<UnidadesIngresos>? UnidadesIngreso = new List<UnidadesIngresos>();

        
        [RelayCommand]
        public async Task<List<UnidadesIngresos>> Gets()
        {

            return await __context.UnidadesIngreso.ToListAsync();
        }
        public async Task Guardar()
        {
            var DatosPruebas = DatosPrueba.Unidades();
            try
            {
                await __context.UnidadesIngreso.AddRangeAsync(DatosPruebas);
                await __context.SaveChangesAsync();
                //Posible error
                await Gets();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Debug.WriteLine(ex.Message);
            }
            Mensaje = "Se logro";
        }

        [RelayCommand]
        //UnidadesIngreso 
        public async Task<Dictionary<string, double>> SumaUnidadesIngreso()
        {
            var list = new Dictionary<string, double>();
            //SIEB
            var SIEB = await __context.UnidadesIngreso.Where(u => u.Rubro == "01").SumAsync(r => r.Recaudado);
            list.Add("01", (double)SIEB);
            //JUnta de asistencia
            var Junta = await __context.UnidadesIngreso.Where(u => u.Rubro == "02").SumAsync(r => r.Recaudado);
            list.Add("02", (double)Junta);
            //HospitalDif
            var Hospital = await __context.UnidadesIngreso.Where(u => u.Rubro == "03").SumAsync(r => r.Recaudado);
            list.Add("03", (double)Hospital);
            //CRIH
            var CRIH = await __context.UnidadesIngreso.Where(u => u.Rubro == "04").SumAsync(r => r.Recaudado);
            list.Add("04", (double)CRIH);
            //Procu
            var Procu = await __context.UnidadesIngreso.Where(u => u.Rubro == "05").SumAsync(r => r.Recaudado);
            list.Add("05", (double)Procu);
            return list;
        }
    }
}
