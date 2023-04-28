using SistVacacionesWeb.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistVacacionesWeb.Domain.RepositoriesContracts
{
    public interface ICentroCostoRepository
    {
        List<CentroCostoModel> ListarCentroCosto(string codEmpresa);
        int GrabarCentroCosto(CentroCostoModel oCentroCostoModel);
        CentroCostoModel RecuperarCentroCosto(string codCentroCosto, string codEmpresa);
        int EliminarCentroCostoFisico(string codCentroCosto, string codEmpresa);
        int EliminarCentroCostoLogico(CentroCostoModel oCentroCostoModel);
    }
}
