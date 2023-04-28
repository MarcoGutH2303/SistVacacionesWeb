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
    public class AreaController : Controller
    {
        private IAreaRepository areaRepository = new AreaRepository(); 
        
        // GET: Area
        [Seguridad]
        public ActionResult Area()
        {
            return View();
        }

        public ActionResult _ListarArea()
        {
            return PartialView();
        }

        public JsonResult ListarArea()
        {
            var listArea = areaRepository.ListarArea(CodEmpresa.Value);
            return Json(new { data = listArea }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CargarArea()
        {
            var listArea = areaRepository.ListarArea(CodEmpresa.Value).Where(x => x.Estado == 1).ToList();
            return Json(listArea, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FiltrarArea(string codCentroCosto)
        {
            var listArea = areaRepository.ListarArea(CodEmpresa.Value).Where(x => x.Estado == 1 && x.CodCentroCosto == codCentroCosto).ToList();
            return Json(listArea, JsonRequestBehavior.AllowGet);
        }

        public int GrabarArea(AreaModel oAreaModel)
        {
            oAreaModel.CodArea = oAreaModel.CodArea == null ? "" : oAreaModel.CodArea;
            oAreaModel.CodEmpresa = CodEmpresa.Value;
            return areaRepository.GrabarArea(oAreaModel);
        }

        public JsonResult RecuperarArea(string codArea)
        {
            return Json(areaRepository.RecuperarArea(codArea, CodEmpresa.Value),
                JsonRequestBehavior.AllowGet);
        }

        public int EliminarAreaLogico(string codArea)
        {
            var oArea = areaRepository.RecuperarArea(codArea, CodEmpresa.Value);
            oArea.EstaBorrado = true;
            return areaRepository.EliminarAreaLogico(oArea);
        }

        public int EliminarAreaFisico(string codArea)
        {
            return areaRepository.EliminarAreaFisico(codArea, CodEmpresa.Value);
        }
    }
}