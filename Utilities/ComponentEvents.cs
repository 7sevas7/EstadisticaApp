using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstadisticaApp.Utilities
{
    public  class ComponentEvents : StateCenter
    {
        public event StateNotify Eventos;
        public void EventoState(object sender, ServiceTitle e)
        {
            if (Eventos != null) {
                Eventos(sender, e);
            }
        }
    }
}
