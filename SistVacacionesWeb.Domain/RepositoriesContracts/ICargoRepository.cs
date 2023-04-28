using SistVacacionesWeb.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistVacacionesWeb.Domain.RepositoriesContracts
{
    public interface ICargoRepository
    {
        List<CargoModel> ListarCargo(string codEmpresa);
        int GrabarCargo(CargoModel oCargoModel);
        CargoModel RecuperarCargo(string codCargo, string codEmpresa);
        int EliminarCargoFisico(string codCargo, string codEmpresa);
        int EliminarCargoLogico(CargoModel oCargoModel);
    }
}
