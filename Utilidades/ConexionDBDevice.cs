using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstadisticaApp.Utilities
{
    public static class ConexionDBDevice
    {
        public static string Ruta(string NombreDB)
        {
            string ruta = string.Empty;
            if (DeviceInfo.Platform == DevicePlatform.Android) {
                ruta = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                ruta = Path.Combine(ruta,NombreDB);
            }else if (DeviceInfo.Platform == DevicePlatform.iOS) {
                ruta = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                ruta = Path.Combine(ruta, "..", "Library", NombreDB);
            }
            else if (DeviceInfo.Platform == DevicePlatform.WinUI )
                {
                ruta = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                ruta = Path.Combine(ruta, NombreDB);
            }
            return ruta;
        }

    }
}
