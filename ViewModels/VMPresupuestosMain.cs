using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EstadisticaApp.ConfigViewModel.Config;
using EstadisticaApp.Controllers;
using EstadisticaApp.Models;
using System.Diagnostics;


namespace EstadisticaApp.ViewModels
{
    public  partial class VMPresupuestosMain:VMBase
    {
        //Clase de implmentacion
        private ControlApiDBEgreso<UnidadesPresupuesto> controlPresupuestos = new ControlApiDBEgreso<UnidadesPresupuesto>();

        [RelayCommand]
        public async Task unidadMes(string rubro)
        {
            ListaXmes = await controlPresupuestos.UnidadXMeses(rubro);
        }
        public async Task<List<double?>>  AcumuladoIngresoUnidad() => await controlPresupuestos.AcumuladoIngresos();

        //Sera un objeto el cual cuente con todas las sumas para la visualización de la grafica 
        [ObservableProperty]
        public UnidadesPresupuesto presupuestoAcumulado;

        [ObservableProperty]
        public List<UnidadesPresupuesto?> listaXmes = new();

        [ObservableProperty]
        public string? mensaje;

        [ObservableProperty]
        public static List<UnidadesPresupuesto?> listaPresupuesto = new();

        public Dictionary<string, List<DataItem>> TotalesDic = new Dictionary<string, List<DataItem>>();
        //Meses letras 

        public async Task<string[]> meses() => await controlPresupuestos.Meses();
        //Recarga de datos 
        public async Task Reload(bool borrar = false) {
            try
            {
                Debug.WriteLine("Entra a reload");
                controlPresupuestos.borrarT = borrar;
                //Para correjir errores
                await controlPresupuestos.Verificar();

                ListaPresupuesto = await controlPresupuestos.AcumuladoUnidad();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public override async Task Loaded()
        {
            ListaPresupuesto = await controlPresupuestos.AcumuladoUnidad();
            TotalesModiEgreso("modificado", "imp_Modificado");
            TotalesModiEgreso("ejercido", "Imp_Comp_Dev_Eje_Pagado");

            addIngresos();
            
        }

        //Manipula el diccionario para el grafico
        //Primero se declara en la funcion TotalesModiEgreso(string);
        public async void addIngresos() {
            List<DataItem> lista = new List<DataItem>();

            int i = 1;
            foreach (var item in await  AcumuladoIngresoUnidad()) {
                lista.Add(new DataItem { Quarter = RubroText(String.Concat("0",i)), Revenue = item});
                i++;
            }
            TotalesDic.Add("ingresos",lista);                   
        }
        //Esta lista es la suma por mes de cada tipo de Egreso , Egreso_Imp_aprobado, egreso_Imp_Ampliacion etc, regresara de a cuerdo al tipo que se de como argumento
        //El argumento es el nombre de la columna, 
        public  List<double?> EgresoMes(string tipo)=> ListaXmes.Select(u => (double?)typeof(UnidadesPresupuesto).GetProperty(tipo).GetValue(u)).ToList();            
              
        public List<double?> EgresoUnidadData(string tipo)
        {
            return ListaPresupuesto.Select(u => (double?)typeof(UnidadesPresupuesto).GetProperty(tipo).GetValue(u)).ToList();
        }

        public string TipoGasto(string tipo)
        {
            switch (tipo)
            {
                case "imp_Modificado":
                    return "Modificado (Egreso)";
                case "Imp_Comp_Dev_Eje_Pagado":
                    return "Ejercido";

                case "Egreso_Imp_aprobado":
                    return "Aprobado";
                default:
                    return "--";
            }

        }
        //Modelo de grafica
        public class DataItem
        {
            public string? Quarter { get; set; }
            public double? Revenue { get; set; }
        }

        public void TotalesModiEgreso(string gasto, string tipo)
        {
            List<DataItem> insertar = new List<DataItem>();
            var tipos = EgresoUnidadData(tipo);
            int i = 0;
            foreach (var item in ListaPresupuesto)
            {
                insertar.Add(new DataItem { Quarter = RubroText(item.Cve_Rubro_Ingreso), Revenue = tipos[i]});
                i++;
                Debug.WriteLine("Aqui se inserta los valores");
            }
            TotalesDic.Add(gasto, insertar);         
        }
        public  string? RubroText(string rubrosCve){
            Dictionary<string, string> Rubrotext = new Dictionary<string, string>{
                {"01","Sistema DIF"},
                {"02","Junta de Asistencia" },
                {"03","Hospital del Niño"},
                {"04","CRIH"},
                {"05","Procuraduría"}
            };
            return Rubrotext[rubrosCve];
        }

    }
}
