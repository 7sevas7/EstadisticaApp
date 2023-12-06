
using EstadisticaApp.Components.Pages;
using EstadisticaApp.ViewModels;

namespace EstadisticaApp.Utilities
{
    public static class Respositories
    {
        public static IServiceCollection AddViewModel(this IServiceCollection service) {

            //Sera por cada view Model creado por interfaz
            service.AddTransient<VMRecUnidadGeneral>();            
            service.AddTransient<VMRecMesUnidad>();
            service.AddTransient<VMPresupuestosMain>();
            return service;
        
        }
    }
}
