using SistVacacionesWeb.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistVacacionesWeb.Domain.RepositoriesContracts
{
    public interface IPersonalRepository
    {
        List<PersonalModel> ListarPersonal(string codEmpresa);
        int GrabarPersonal(PersonalModel oPersonalModel);
        PersonalModel RecuperarPersonal(string codPersonal, string codEmpresa);
        int EliminarPersonalFisico(string codPersonal, string codEmpresa);
        int EliminarPersonalLogico(PersonalModel oPersonalModel);
    }
}
