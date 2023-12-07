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

        [ObservableProperty]
        public List<double?> acumuladoIngresoUnidad = new();
        public override async Task Loaded()
        {

           
          // await presupuestos.InserPrueba();
               
           

            var MeSes = await presupuestos.Meses();
            Meses = MeSes.ToArray();
            
            AcumuladoIngresoUnidad = await presupuestos.AcumuladoIngresos();
            AcumuladoUnidad();
            //Funcion la cual devuelve lista por cada rubro con su suma correspondiente 
            ListaPresupuesto = await presupuestos.AcumuladoUnidad();
            
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
        public async Task AcumuladoUnidad()
        {
           AcumuladoIngresoUnidad =  await presupuestos.AcumuladoIngresos();
        }
        //Lo podremos definir en un estado

        //Suma de presupuestos de unidad
        public List<double?> EgresoUnidadData(string tipo)
        {
            return ListaPresupuesto.Select(u => (double?)typeof(UnidadesPresupuesto).GetProperty(tipo).GetValue(u)).ToList();
        }

        public string[] ListaUnidades = {
         "Sistema DIF","Junta de Asistencia","Hostipal de Niño","CRIH","Procuraduría"
        };
        public string TipoGasto(string tipo)
        {
            switch (tipo)
            {
                case "imp_Modificado":
                    return "Modificado";
                case "Imp_Comp_Dev_Eje_Pagado":
                    return "Egreso";
                case "Egreso_Imp_aprobado":
                    return "Aprobado";
                default:
                    return "--";
            }

        }


    }
}
