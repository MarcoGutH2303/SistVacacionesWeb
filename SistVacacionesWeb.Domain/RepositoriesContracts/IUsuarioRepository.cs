using SistVacacionesWeb.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistVacacionesWeb.Domain.RepositoriesContracts
{
    public interface IUsuarioRepository
    {
        List<UsuarioModel> ListarUsuario(string codEmpresa);
        int GrabarUsuario(UsuarioModel oUsuarioModel, RolModel oRolModel);
        UsuarioModel RecuperarUsuario(string codUsuario, string codEmpresa);
        int EliminarUsuarioFisico(string codUsuario, string codEmpresa);
        int EliminarUsuarioLogico(UsuarioModel oUsuarioModel, RolModel oRolModel);
    }
}
