using SistVacacionesWeb.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistVacacionesWeb.Domain.RepositoriesContracts
{
    public interface IPanelVacacionesRepository
    {
        List<PanelVacacionesPeriodoModel> ListarVacacionesPeriodo(string codPersonal, string codEmpresa);
        List<PanelVacacionesConsumoModel> ListarVacacionesConsumo(string codVacacionesPeriodo, string codPersonal, string codEmpresa);
    }
}
