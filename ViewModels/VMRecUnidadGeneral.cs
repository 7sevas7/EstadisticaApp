using EstadisticaApp.Models;
using System.Diagnostics;
using EstadisticaApp.ConfigViewModel.Config;
using CommunityToolkit.Mvvm.Input;

using EstadisticaApp.Controllers;


namespace EstadisticaApp.Components.Pages
{
    public partial class VMRecUnidadGeneral : VMBase
    {

        ControlApiDBIngreso<UnidadesIngresos> controlApiDBIngreso = new ControlApiDBIngreso<UnidadesIngresos>();

        public string Mensaje { set; get; }
        public List<UnidadesIngresos>? UnidadesIngreso = new List<UnidadesIngresos>();

        [RelayCommand]
        public async Task<List<UnidadesIngresos>> Gets()
        {
            return await controlApiDBIngreso.Getss();
            
        }
        public async Task Reload() {
            Debug.WriteLine("Se recarga");
            await controlApiDBIngreso.ClearTable();
            
            await controlApiDBIngreso.VerificarData();            
            var vacio = controlApiDBIngreso.BoolCount;
            
            if (vacio) {
                Debug.WriteLine("Correcto");
            }
            else
            {
                Debug.WriteLine("No crrectt");
            }

            
           
        
        
        }

        [RelayCommand]
        //UnidadesIngreso 
        public async Task<List<double>> SumaUnidadesIngreso()
        {
            //Sera objeto de clase Recaudo
            return await controlApiDBIngreso.UnidadSuma();
        }
    }
}
