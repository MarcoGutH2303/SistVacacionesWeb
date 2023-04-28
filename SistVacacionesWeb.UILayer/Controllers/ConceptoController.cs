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
    public class ConceptoController : Controller
    {
        private IConceptoRepository conceptoRepository = new ConceptoRepository();

        // GET: Concepto
        [Seguridad]
        public ActionResult Concepto()
        {
            return View();
        }

        public ActionResult _ListarConcepto()
        {
            return PartialView();
        }

        public JsonResult ListarConcepto()
        {
            var listConcepto = conceptoRepository.ListarConcepto(CodEmpresa.Value);
            return Json(new { data = listConcepto }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CargarConcepto()
        {
            var listConcepto = conceptoRepository.ListarConcepto(CodEmpresa.Value).Where(x => x.Estado == 1).ToList();
            return Json(listConcepto, JsonRequestBehavior.AllowGet);
        }

        public int GrabarConcepto(ConceptoModel oConceptoModel)
        {
            oConceptoModel.CodConcepto = oConceptoModel.CodConcepto == null ? "" : oConceptoModel.CodConcepto;
            oConceptoModel.CodEmpresa = CodEmpresa.Value;
            return conceptoRepository.GrabarConcepto(oConceptoModel);
        }

        public JsonResult RecuperarConcepto(string codConcepto)
        {
            return Json(conceptoRepository.RecuperarConcepto(codConcepto, CodEmpresa.Value),
                JsonRequestBehavior.AllowGet);
        }

        public int EliminarConceptoLogico(string codConcepto)
        {
            var oConcepto = conceptoRepository.RecuperarConcepto(codConcepto, CodEmpresa.Value);
            oConcepto.EstaBorrado = true;
            return conceptoRepository.EliminarConceptoLogico(oConcepto);
        }

        public int EliminarConceptoFisico(string codConcepto)
        {
            return conceptoRepository.EliminarConceptoFisico(codConcepto, CodEmpresa.Value);
        }
    }
}