using SistVacacionesWeb.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistVacacionesWeb.Domain.RepositoriesContracts
{
    public interface IAreaRepository
    {
        List<AreaModel> ListarArea(string codEmpresa);
        int GrabarArea(AreaModel oAreaModel);
        AreaModel RecuperarArea(string codArea, string codEmpresa);
        int EliminarAreaFisico(string codArea, string codEmpresa);
        int EliminarAreaLogico(AreaModel oAreaModel);
    }
}
