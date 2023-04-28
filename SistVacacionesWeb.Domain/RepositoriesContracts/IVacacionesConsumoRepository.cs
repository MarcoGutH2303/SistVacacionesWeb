using SistVacacionesWeb.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistVacacionesWeb.Domain.RepositoriesContracts
{
    public interface IVacacionesConsumoRepository
    {
        List<VacacionesConsumoModel> ListarVacacionesConsumo(string codVacacionesPeriodo, string codPersonal, string codEmpresa);
        int GrabarVacacionesConsumo(VacacionesConsumoModel oVacacionesConsumoModel);
        VacacionesConsumoModel RecuperarVacacionesConsumo(string codVacacionesConsumo, string codVacacionesPeriodo, string codPersonal, string codEmpresa);
        int EliminarVacacionesConsumoFisico(string codVacacionesConsumo, string codVacacionesPeriodo, string codPersonal, string codEmpresa);
        int EliminarVacacionesConsumoLogico(VacacionesConsumoModel oVacacionesConsumoModel);
    }
}
