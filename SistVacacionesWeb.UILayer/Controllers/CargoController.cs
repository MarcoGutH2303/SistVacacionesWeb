using SistVacacionesWeb.DataAccessLayer.Repository;
using SistVacacionesWeb.Domain.Models;
using SistVacacionesWeb.Domain.RepositoriesContracts;
using SistVacacionesWeb.UILayer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistVacacionesWeb.UILayer.Controllers
{
    public class CargoController : Controller
    {
        private ICargoRepository cargoRepository = new CargoRepository();

        // GET: Cargo
        [Seguridad]
        public ActionResult Cargo()
        {
            return View();
        }

        public ActionResult _ListarCargo()
        {
            return PartialView();
        }

        public JsonResult ListarCargo()
        {
            var listCargo = cargoRepository.ListarCargo(CodEmpresa.Value);
            return Json(new { data = listCargo }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CargarCargo()
        {
            var listCargo = cargoRepository.ListarCargo(CodEmpresa.Value).Where(x => x.Estado == 1).ToList();
            return Json(listCargo, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FiltrarCargo(string codArea)
        {
            var listArea = cargoRepository.ListarCargo(CodEmpresa.Value).Where(x => x.Estado == 1 && x.CodArea == codArea).ToList();
            return Json(listArea, JsonRequestBehavior.AllowGet);
        }

        public int GrabarCargo(CargoModel oCargoModel)
        {
            oCargoModel.CodCargo = oCargoModel.CodCargo == null ? "" : oCargoModel.CodCargo;
            oCargoModel.CodEmpresa = CodEmpresa.Value;
            return cargoRepository.GrabarCargo(oCargoModel);
        }

        public JsonResult RecuperarCargo(string codCargo)
        {
            return Json(cargoRepository.RecuperarCargo(codCargo, CodEmpresa.Value),
                JsonRequestBehavior.AllowGet);
        }

        public int EliminarCargoLogico(string codCargo)
        {
            var oCargo = cargoRepository.RecuperarCargo(codCargo, CodEmpresa.Value);
            oCargo.EstaBorrado = true;
            return cargoRepository.EliminarCargoLogico(oCargo);
        }

        public int EliminarCargoFisico(string codCargo)
        {
            return cargoRepository.EliminarCargoFisico(codCargo, CodEmpresa.Value);
        }
    }
}