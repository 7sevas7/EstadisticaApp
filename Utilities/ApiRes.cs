using EstadisticaApp.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Diagnostics;

namespace EstadisticaApp.Utilities
{
    
    public class ApiRes
    {

        HttpClient httpClient;
        

        public ApiRes()
        {
            //Recibira la base de la ruta que utilizara
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://172.16.4.25:80/apiPruebas/api/");
           // baseUrl = "http://172.16.4.25:80/apiPruebas/api/";
                       

        }
        public async Task<List<UnidadesPresupuesto>?> GetsListPresupuesto(string rubro){
            Debug.WriteLine("De peticion");
            
            List<UnidadesPresupuesto>? lista = new();
            try
            {
                int rubroId = Convert.ToInt32(rubro);
                Debug.WriteLine("Datos de Api Pre");
               // string uri = $"{baseUrl}Egresos/{rubro}";
                HttpResponseMessage res = await httpClient.GetAsync($"Egresos/{rubro}");
                
<<<<<<< HEAD
                    
=======

>>>>>>> PruebasIngreso
                    string respuesta = await res.Content.ReadAsStringAsync();
                    //Debug.WriteLine(respuesta);
                    lista = JsonConvert.DeserializeObject<List<UnidadesPresupuesto>>(respuesta);
                
            }
            catch (Exception ex) {
                
                Debug.Write("Errror==>"+ex);
            }
                
            return lista;
            
}




    }
}
