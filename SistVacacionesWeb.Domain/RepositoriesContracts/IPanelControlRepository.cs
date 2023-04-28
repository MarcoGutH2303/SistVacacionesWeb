using SistVacacionesWeb.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistVacacionesWeb.Domain.RepositoriesContracts
{
    public interface IPanelControlRepository
    {
        PanelControlAdministradorModel RecuperarPanelControlAdministrador(string codEmpresa);
        PanelControlEmpleadoModel RecuperarPanelControlEmpleado(string codPersonal, string codEmpresa);
    }
}
