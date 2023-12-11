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
            httpClient.BaseAddress = new Uri("http://localhost:5082/");
        }
        public async Task<List<UnidadesPresupuesto>?> GetsListPresupuesto(){
            Debug.WriteLine("De peticion");
            
            List<UnidadesPresupuesto>? lista = new();
            try
            {
                Debug.WriteLine("Datos de Api Pre");
                HttpResponseMessage res = await httpClient.GetAsync("api/Egresos");
                
                    Debug.WriteLine("Datos de Api Post");
                    string respuesta = await res.Content.ReadAsStringAsync();
                    Debug.WriteLine(respuesta);
                    lista = JsonConvert.DeserializeObject<List<UnidadesPresupuesto>>(respuesta);
                    
                
                
            }
            catch (Exception ex) {
                Debug.Write("Errror==>"+ex.Message);
            }
                
            return lista;
            
}




    }
}
