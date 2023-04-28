using SistVacacionesWeb.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistVacacionesWeb.Domain.RepositoriesContracts
{
    public interface IRolRepository
    {
        List<RolModel> ListarRol(string codUsuario, string codEmpresa);
        int GrabarRol(RolModel oRolModel);
        RolModel RecuperarRol(string codUsuario, string codEmpresa);
        int EliminarRolFisico(string codUsuario, string codEmpresa);
        int EliminarRolLogico(RolModel oRolModel);
    }
}
