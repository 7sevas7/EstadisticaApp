using EstadisticaApp.Models;
using System.Diagnostics;
using EstadisticaApp.ConfigViewModel.Config;
using CommunityToolkit.Mvvm.Input;
using EstadisticaApp.DataAcces.Implement;


namespace EstadisticaApp.Components.Pages
{
    public partial class VMRecUnidadGeneral : VMBase
    {


        
        private readonly ConsultaGeneral<UnidadesIngresos> __context = new();
        public string Mensaje { set; get; }
        public List<UnidadesIngresos>? UnidadesIngreso = new List<UnidadesIngresos>();

      

        [RelayCommand]
        public async Task<List<UnidadesIngresos>> Gets()
        {
            return await __context.Get();
            
        }

        public async Task Guardar()
        {
            var DatosPruebas = DatosPrueba.Unidades();
            try
            {
                await __context.Insert(DatosPruebas);                
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
        public async Task<List<double>> SumaUnidadesIngreso()
        {
            return await __context.UnidadSuma();
        }
    }
}
