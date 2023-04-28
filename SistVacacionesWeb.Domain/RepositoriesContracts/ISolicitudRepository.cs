using SistVacacionesWeb.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistVacacionesWeb.Domain.RepositoriesContracts
{
    public interface ISolicitudRepository
    {
        List<SolicitudModel> ListarSolicitud(string codEmpresa);
        int GrabarSolicitud(SolicitudModel oSolicitudModel);
        SolicitudModel RecuperarSolicitud(string codSolicitud, string codEmpresa);
        int EliminarSolicitudFisico(string codSolicitud, string codEmpresa);
        int EliminarSolicitudLogico(SolicitudModel oSolicitudModel);
    }
}
