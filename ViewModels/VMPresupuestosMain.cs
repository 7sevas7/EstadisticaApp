using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using EstadisticaApp.ConfigViewModel.Config;
using EstadisticaApp.Controllers;
using EstadisticaApp.DataAcces.Implement;
using EstadisticaApp.Models;
using System.Diagnostics;


namespace EstadisticaApp.ViewModels
{
    public  partial class VMPresupuestosMain:VMBase
    {
        //Clase de implmentacion
        private ControlApiDBEgreso<UnidadesPresupuesto> controlPresupuestos = new ControlApiDBEgreso<UnidadesPresupuesto>();
        //PresupuestoMain presupuestos = new PresupuestoMain();

        //Control de Api 
        //private readonly ControlApiDBIngreso<UnidadesIngresos> controlIngresos = new ControlApiDBIngreso<UnidadesIngresos>();

        //Sera un objeto el cual cuente con todas las sumas para la visualización de la grafica 
        [ObservableProperty]
        public UnidadesPresupuesto presupuestoAcumulado;

        [ObservableProperty]
        public List<UnidadesPresupuesto?> listaXmes = new();

        [ObservableProperty]
        public string? mensaje;

        [ObservableProperty]
        public List<UnidadesPresupuesto?> listaPresupuesto = new();

        //Meses letras 
        [ObservableProperty]
        public string[] meses;

        [ObservableProperty]
        public List<double?> acumuladoIngresoUnidad = new();
      
        [ObservableProperty]
        public bool observerender = false;

        //Recarga de datos 
        public async Task Reload() {
            await controlPresupuestos.ClearTable();
            await controlPresupuestos.VerificarData();
            var vacio = controlPresupuestos.BoolCount;
            
        }
        

        public override async Task Loaded()
        {
            //Para correjir errores
            await controlPresupuestos.VerificarData();

            Observerender = controlPresupuestos.BoolCount;
            if (!Observerender) {
                
            //    Meses = await presupuestos.Meses();
                
                AcumuladoIngresoUnidad = await controlPresupuestos.AcumuladoIngresos();//>>>>>>>>Este solo de las
            //    //Funcion la cual devuelve lista por cada rubro con su suma correspondiente 
               ListaPresupuesto = await controlPresupuestos.AcumuladoUnidad();
            }
          
           

        
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
            ListaXmes = await controlPresupuestos.UnidadXMeses(rubro);
             

        }

        [RelayCommand]
        public async Task AcumuladoUnidad()
        {
           AcumuladoIngresoUnidad =  await controlPresupuestos.AcumuladoIngresos();
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

        //Se borraran los dato, para evitar información con mala consistencia 
        public async Task ClearTable() {
            Debug.WriteLine("Se borra");
            await controlPresupuestos.ClearTable();
        }



    }
}
