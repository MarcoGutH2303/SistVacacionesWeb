using SistVacacionesWeb.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistVacacionesWeb.Domain.RepositoriesContracts
{
    public interface IAutorizacionRepository
    {
        List<AutorizacionModel> ListarAutorizacion(string codEmpresa);
        int GrabarAutorizacion(AutorizacionModel oAutorizacionModel);
        AutorizacionModel RecuperarAutorizacion(string codAutorizacion, string codEmpresa);
        int EliminarAutorizacionFisico(string codAutorizacion, string codEmpresa);
        int EliminarAutorizacionLogico(AutorizacionModel oAutorizacionModel);
    }
}
