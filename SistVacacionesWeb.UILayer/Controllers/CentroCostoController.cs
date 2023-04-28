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
    public class CentroCostoController : Controller
    {
        private ICentroCostoRepository centroCostoRepository = new CentroCostoRepository();
        
        // GET: CentroCosto
        [Seguridad]
        public ActionResult CentroCosto()
        {
            return View();
        }

        public ActionResult _ListarCentroCosto()
        {
            return PartialView();
        }

        public JsonResult ListarCentroCosto()
        {
            var listCentroCosto = centroCostoRepository.ListarCentroCosto(CodEmpresa.Value);
            return Json(new { data = listCentroCosto }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CargarCentroCosto()
        {
            var listCentroCosto = centroCostoRepository.ListarCentroCosto(CodEmpresa.Value).Where(x => x.Estado == 1).ToList();
            return Json(listCentroCosto, JsonRequestBehavior.AllowGet);
        }

        public int GrabarCentroCosto(CentroCostoModel oCentroCostoModel)
        {
            oCentroCostoModel.CodCentroCosto = oCentroCostoModel.CodCentroCosto == null ? "" : oCentroCostoModel.CodCentroCosto;
            oCentroCostoModel.CodigoAnterior = oCentroCostoModel.CodigoAnterior == null ? "" : oCentroCostoModel.CodigoAnterior;
            oCentroCostoModel.CodEmpresa = CodEmpresa.Value;
            return centroCostoRepository.GrabarCentroCosto(oCentroCostoModel);
        }

        public JsonResult RecuperarCentroCosto(string codCentroCosto)
        {
            return Json(centroCostoRepository.RecuperarCentroCosto(codCentroCosto, CodEmpresa.Value),
                JsonRequestBehavior.AllowGet);
        }

        public int EliminarCentroCostoLogico(string codCentroCosto)
        {
            var oCentroCosto = centroCostoRepository.RecuperarCentroCosto(codCentroCosto, CodEmpresa.Value);
            oCentroCosto.EstaBorrado = true;
            return centroCostoRepository.EliminarCentroCostoLogico(oCentroCosto);
        }

        public int EliminarCentroCostoFisico(string codCentroCosto)
        {
            return centroCostoRepository.EliminarCentroCostoFisico(codCentroCosto, CodEmpresa.Value);
        }
    }
}