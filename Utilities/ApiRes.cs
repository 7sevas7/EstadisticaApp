
using EstadisticaApp.DataAcces.Implement;
using Newtonsoft.Json;
using System.Diagnostics;


namespace EstadisticaApp.Utilities
{
    
    public class ApiRes<Modeli> where Modeli : class
    {

        HttpClient httpClient;
        ConsultaGeneral<Modeli> consultaGeneral  = new ConsultaGeneral<Modeli>();

        public ApiRes()
        {
            //Recibira la base de la ruta que utilizara
            httpClient = new HttpClient();
            
            //httpClient.BaseAddress = new Uri("http://192.168.1.67/apiPruebas/api/");
            httpClient.BaseAddress = new Uri("http://172.16.4.25:80/apiPruebas/api/");
            // baseUrl = "http://172.16.4.25:80/apiPruebas/api/";


        }
        public async Task<List<Modeli>?> GetsList(string tabla,string rubro){
            Debug.WriteLine("De peticion");
            
            List<Modeli>? lista = new();
            try
            {
                int rubroId = Convert.ToInt32(rubro);
                Debug.WriteLine("Datos de Api Pre");
               // string uri = $"{baseUrl}Egresos/{rubro}";
                HttpResponseMessage res = await httpClient.GetAsync($"{tabla}/{rubro}");

                if (res.IsSuccessStatusCode ) {
                    
                    string respuesta = await res.Content.ReadAsStringAsync();
                    //Debug.WriteLine(respuesta);

                    lista = JsonConvert.DeserializeObject<List<Modeli>>(respuesta);
                    //lista = JsonSerializer.Deserialize<List<Modeli>>(respuesta);
                }
                    
                
            }
            catch (Exception ex) {
                
                Debug.Write("Errror==>"+ex);
            }
                
            return lista;
            
}




    }
}
