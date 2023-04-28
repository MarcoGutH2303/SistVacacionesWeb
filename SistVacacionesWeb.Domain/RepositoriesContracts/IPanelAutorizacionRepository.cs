using SistVacacionesWeb.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistVacacionesWeb.Domain.RepositoriesContracts
{
    public interface IPanelAutorizacionRepository
    {
        List<PanelAutorizacionModel> ListarAutorizacionPersonal(string codPersonal, string codEmpresa);
        PanelAutorizacionModel RecuperarAutorizacionPersonal(string codAutorizacion, string codPersonal, string codEmpresa);
    }
}
