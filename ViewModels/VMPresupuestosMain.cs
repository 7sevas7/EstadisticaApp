﻿using CommunityToolkit.Mvvm.ComponentModel;
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
        public string[] meses = { };

        [ObservableProperty]
        public List<double?> acumuladoIngresoUnidad = new();
      
        
        public bool borrandoT { set; get; } = false;
        //Recarga de datos 
        public async Task Reload() {
            try
            {
                Debug.WriteLine("Entra a reload");
                controlPresupuestos.borrarT = borrandoT;
                //Para correjir errores
                await controlPresupuestos.Verificar();

                Meses = await controlPresupuestos.Meses();
                AcumuladoIngresoUnidad = await controlPresupuestos.AcumuladoIngresos();//>>>>>>>>Este solo de las
                                                                                       //    //Funcion la cual devuelve lista por cada rubro con su suma correspondiente 
                ListaPresupuesto = await PresupuestoLista();
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<UnidadesPresupuesto>> PresupuestoLista()=> await controlPresupuestos.AcumuladoUnidad();

        //public override async Task Loaded()
        //{
        //    Debug.WriteLine("Entra a reload");
        //    //Para correjir errores
        //    await controlPresupuestos.VerificarData();

        //    Observerender = controlPresupuestos.BoolCount;

        //    Meses = await controlPresupuestos.Meses();
        //    AcumuladoIngresoUnidad = await controlPresupuestos.AcumuladoIngresos();//>>>>>>>>Este solo de las
        //                                                                           //    //Funcion la cual devuelve lista por cada rubro con su suma correspondiente 
        //    ListaPresupuesto = await controlPresupuestos.AcumuladoUnidad();

        //}
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
                    return "Modificado (Egreso)";
                case "Imp_Comp_Dev_Eje_Pagado":
                    return "Ejercido";

                case "Egreso_Imp_aprobado":
                    return "Aprobado";
                default:
                    return "--";
            }

        }

        //Se borraran los dato, para evitar información con mala consistencia 
      



    }
}
