using SistVacacionesWeb.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistVacacionesWeb.Domain.RepositoriesContracts
{
    public interface IEmpresaRepository
    {
        List<EmpresaModel> ListarEmpresa();
        int GrabarEmpresa(EmpresaModel oEmpresaModel, UsuarioModel oUsuarioModel, RolModel oRolModel);
        EmpresaModel RecuperarEmpresa(string codEmpresa);
        int EliminarEmpresaFisico(string codEmpresa);
        int EliminarEmpresaLogico(EmpresaModel oEmpresaModel);
    }
}
