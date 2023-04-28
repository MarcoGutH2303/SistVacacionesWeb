using SistVacacionesWeb.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistVacacionesWeb.Domain.RepositoriesContracts
{
    public interface IVacacionesPeriodoRepository
    {
        List<VacacionesPeriodoModel> ListarVacacionesPeriodo(string codPersonal, string codEmpresa);
        int GrabarVacacionesPeriodo(VacacionesPeriodoModel oVacacionesPeriodoModel);
        VacacionesPeriodoModel RecuperarVacacionesPeriodo(string codVacacionesPeriodo, string codPersonal, string codEmpresa);
        int EliminarVacacionesPeriodoFisico(string codVacacionesPeriodo, string codPersonal, string codEmpresa);
        int EliminarVacacionesPeriodoLogico(VacacionesPeriodoModel oVacacionesPeriodoModel);
    }
}
