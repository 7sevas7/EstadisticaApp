
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EstadisticaApp.ConfigViewModel.Config;
using EstadisticaApp.DataAcces.Implement;
using EstadisticaApp.Models;


namespace EstadisticaApp.ViewModels
{
    public  partial class VMPresupuestosMain:VMBase
    {

        PresupuestoMain presupuestos = new PresupuestoMain();

        //Sera un objeto el cual cuente con todas las sumas para la visualización de la grafica 
        [ObservableProperty]
        public UnidadesPresupuesto presupuestoAcumulado;

        [ObservableProperty]
        public List<UnidadesPresupuesto>? listaXmes = new();

        [ObservableProperty]
        public string? mensaje;

        [ObservableProperty]
        public List<UnidadesPresupuesto>? listaPresupuesto = new();

        //Meses letras 
        [ObservableProperty]
        public string[] meses;
        public override async Task Loaded()
        {
            var MeSes = await presupuestos.Meses();
            Meses = MeSes.ToArray();
            AcumladoUnidad();
            
        }
        //Esta lista es la suma por mes de cada tipo de Egreso , Egreso_Imp_aprobado, egreso_Imp_Ampliacion etc, regresara de a cuerdo al tipo que se de como argumento
        //El argumento es el nombre de la columna, 
        public  List<double?> EgresoMes(string tipo)
        {            
            return  ListaXmes.Select(u => (double?)typeof(UnidadesPresupuesto).GetProperty(tipo).GetValue(u)).ToList();            
        }

        [RelayCommand]
        public async Task unidadMes(string rubro)
        {
            await presupuestos.UnidadXMeses(rubro);
            ListaXmes = presupuestos.UnidadXMes;

        }

        [RelayCommand]
        public async Task AcumladoUnidad()
        {
           ListaPresupuesto =  await presupuestos.AcumuladoUnidad();
        }
        //Lo podremos definir en un estado
        


    }
}
