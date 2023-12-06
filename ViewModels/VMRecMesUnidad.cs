using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EstadisticaApp.ConfigViewModel.Config;
using EstadisticaApp.DataAcces.Implement;

using EstadisticaApp.Models;
using System.Collections.ObjectModel;


namespace EstadisticaApp.ViewModels
{
    public partial class VMRecMesUnidad : VMBase
    {
        UnidadesMes BaseUnidades = new UnidadesMes();
        [ObservableProperty]
        public string[]? idUnidad;

        [ObservableProperty]
        public string[]? meses;
        
        
        [ObservableProperty]
        public List<UnidadesIngresos> recaudoMes = new ();

        
        public override async Task Loaded()
        {
            //Meses = await BaseUnidades.Meses();           
            //Meses = mesesLabels.ToArray();
            Meses = await BaseUnidades.mesesUpper();
           // Unidades = await BaseUnidades.GetMesePorUnidad("01");
        }

        [RelayCommand]
        public virtual async Task datosPorRubro(string rubro)
        {
            RecaudoMes = await BaseUnidades.GetMesePorUnidad(rubro);
        }
    }
}
