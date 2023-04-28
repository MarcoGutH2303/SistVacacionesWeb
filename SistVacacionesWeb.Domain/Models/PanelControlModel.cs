using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistVacacionesWeb.Domain.Models
{
    public class PanelControlAdministradorModel
    {
        public int CantSolicitudPendiente { get; set; }
        public int CantSolicitudResuelto { get; set; }
        public int CantAutorizacionRealizado { get; set; }
        public int CantVacacionesPeriodo { get; set; }
    }

    public class PanelControlEmpleadoModel
    {
        public int CantSolicitudPendiente { get; set; }
        public int CantAutorizacionRealizado { get; set; }
        public int CantVacacionesPeriodo { get; set; }
    }
}
