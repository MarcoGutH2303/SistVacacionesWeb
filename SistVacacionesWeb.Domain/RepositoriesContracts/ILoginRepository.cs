using SistVacacionesWeb.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistVacacionesWeb.Domain.RepositoriesContracts
{
    public interface ILoginRepository
    {
        List<LoginModel> ListaMenuCabecerasAdministrador();
        List<LoginModel> ListaMenuCabecerasEmpleado();
        List<LoginModel> ListaMenuHijosAdministrador();
        string ValidarLogin(string usuario, string pass, string codEmpresa);
        int RecuperarLogin(string codUsuario, string codEmpresa);
    }
}
