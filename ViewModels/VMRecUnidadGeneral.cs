using EstadisticaApp.Models;
using System.Diagnostics;
using EstadisticaApp.ConfigViewModel.Config;
using CommunityToolkit.Mvvm.Input;

using EstadisticaApp.Controllers;


namespace EstadisticaApp.Components.Pages
{
    public partial class VMRecUnidadGeneral : VMBase
    {
        public bool borrandoT = false;

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
            //Verificar primero la conexión
            controlApiDBIngreso.borrarT = borrandoT;
            await controlApiDBIngreso.VerificarData();            
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
